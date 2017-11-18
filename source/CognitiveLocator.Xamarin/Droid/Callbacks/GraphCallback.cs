using System;
using Xamarin.Facebook;

namespace CognitiveLocator.Droid.Callbacks
{
    class GraphCallback : Java.Lang.Object, GraphRequest.ICallback
    {
        // Event to pass the response when it's completed
        public event EventHandler<GraphResponseEventArgs> RequestCompleted = delegate { };

        public void OnCompleted(GraphResponse reponse)
        {
            this.RequestCompleted(this, new GraphResponseEventArgs(reponse));
        }
    }

    public class GraphResponseEventArgs : EventArgs
    {
        GraphResponse _response;
        public GraphResponseEventArgs(GraphResponse response)
        {
            _response = response;
        }

        public GraphResponse Response { get { return _response; } }
    }
}
