using BlazingPizza.ComponentsLibrary.Map;
using System;
using System.Collections.Generic;

namespace BlazingPizza
{
    public class OrderWithStatus
    {
        public Order Order { get; set; }

        public string StatusText { get; set; }

        public List<Marker> MapMarkers { get; set; }

        public static OrderWithStatus FromOrder(Order order)
        {
            List<Marker> mapMarkers = new List<Marker>();
            var dispatchTime = order.CreatedTime.AddSeconds(10);
            var deliveryDuration = TimeSpan.FromMinutes(1); // Unrealistic, but more interesting to watch

            // To simulate a real backend process, we fake status updates based on the amount
            // of time since the order was placed
            string statusText;
            if (DateTime.Now < dispatchTime)
            {
                statusText = "Preparing";
                mapMarkers.Add(ToMapMarker("You", order.DeliveryLocation, showPopup: true));
            }
            else if (DateTime.Now < dispatchTime + deliveryDuration)
            {
                statusText = "Out for delivery";

                var startPosition = ComputeStartPosition(order);
                var proportionOfDeliveryCompleted = Math.Min(1, (DateTime.Now - dispatchTime).TotalMilliseconds / deliveryDuration.TotalMilliseconds);
                var driverPosition = LatLong.Interpolate(startPosition, order.DeliveryLocation, proportionOfDeliveryCompleted);
                mapMarkers.AddRange(new List<Marker>
                {
                    ToMapMarker("You", order.DeliveryLocation),
                    ToMapMarker("Driver", driverPosition, showPopup: true),
                });
            }
            else
            {
                statusText = "Delivered";
                mapMarkers.Add(ToMapMarker("Delivery location", order.DeliveryLocation, showPopup: true));
            }

            var orderWithStatus = new OrderWithStatus()
            {
                Order = order,
                StatusText = statusText,
                MapMarkers = mapMarkers,
            };

            return orderWithStatus;
        }

        private static LatLong ComputeStartPosition(Order order)
        {
            // Random but deterministic based on order ID
            var rng = new Random(order.OrderId);
            var distance = 0.01 + rng.NextDouble() * 0.02;
            var angle = rng.NextDouble() * Math.PI * 2;
            var offset = (distance * Math.Cos(angle), distance * Math.Sin(angle));
            var latLong = new LatLong(order.DeliveryLocation.Latitude + offset.Item1, order.DeliveryLocation.Longitude + offset.Item2);
            return latLong;
        }

        static Marker ToMapMarker(string description, LatLong coords, bool showPopup = false)
        {
            var marker = new Marker
            {
                Description = description,
                X = coords.Longitude,
                Y = coords.Latitude,
                ShowPopup = showPopup
            };
            return marker;
        }
    }
}
