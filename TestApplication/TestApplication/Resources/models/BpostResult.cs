using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TestApplication.Resources.models
{
    public class Key
    {
        public long id { get; set; }
        public long created { get; set; }
        public int version { get; set; }
        public string primaryKey { get; set; }
    }

    public class OrderDate
    {
        public string day { get; set; }
        public string time { get; set; }
    }

    public class Eligibilities
    {
        public bool eligibileForNeighbourDelivery { get; set; }
        public bool eligibileForSafeplaceDelivery { get; set; }
        public bool eligibileForPreferedAvisage { get; set; }
    }

    public class LatestAvailableTime
    {
        public string day { get; set; }
    }

    public class ActualDeliveryTime
    {
        public string day { get; set; }
    }

    public class Sender
    {
        public string countryCode { get; set; }
        public string pdpId { get; set; }
    }

    public class Receiver
    {
        public string countryCode { get; set; }
        public string pdpId { get; set; }
    }

    public class Label
    {
        public string main { get; set; }
        public string detail { get; set; }
    }

    public class ProcessStep
    {
        public string name { get; set; }
        public string status { get; set; }
        public Label label { get; set; }
        public string knownProcessStep { get; set; }
    }

    public class ProcessOverview
    {
        public string activeStepTextKey { get; set; }
        public string textKey { get; set; }
        public bool alert { get; set; }
        public List<ProcessStep> processSteps { get; set; }
        public string helpTextKey { get; set; }
    }

    public class Event
    {
        public string date { get; set; }
        public string time { get; set; }
        public string key { get; set; }
        public bool irregularity { get; set; }
    }

    public class Title
    {
        public string fr { get; set; }
        public string nl { get; set; }
        public string en { get; set; }
    }

    public class Faq
    {
        public string id { get; set; }
        public Title title { get; set; }
    }

    public class Item
    {
        public Key key { get; set; }
        public string itemCode { get; set; }
        public OrderDate orderDate { get; set; }
        public bool activeForDeliveryPreferences { get; set; }
        public Eligibilities eligibilities { get; set; }
        public string inNetworkDate { get; set; }
        public string productCategory { get; set; }
        public bool retourOrBackToSender { get; set; }
        public string contactForMoreInformation { get; set; }
        public LatestAvailableTime latestAvailableTime { get; set; }
        public ActualDeliveryTime actualDeliveryTime { get; set; }
        public Sender sender { get; set; }
        public Receiver receiver { get; set; }
        public ProcessOverview processOverview { get; set; }
        public List<Event> events { get; set; }
        public string requestedDeliveryMethod { get; set; }
        public string product { get; set; }
        public List<object> services { get; set; }
        public List<Faq> faqs { get; set; }
        public string signatureViewType { get; set; }
        public bool showFullAddressLink { get; set; }
        public string webformUrl { get; set; }
    }

    public class RootObject
    {
        public List<Item> items { get; set; }
        public List<object> listOfParcelItemsByEmailIds { get; set; }
    }
}