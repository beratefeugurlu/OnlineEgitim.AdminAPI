﻿namespace OnlineEgitim.AdminAPI.Models
{
    public class PaymentResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int? OrderId { get; set; }
    }
}
