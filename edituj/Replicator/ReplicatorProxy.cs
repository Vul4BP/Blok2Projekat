using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Replicator
{
    public class ReplicatorProxy : ChannelFactory<IReplicator>, IReplicator, IDisposable
    {
        IReplicator factory;


        public ReplicatorProxy(NetTcpBinding binding,string address):base(binding,address)
        {        
                factory = this.CreateChannel();        
        }

        public List<Element> ListAllElements(string name)
        {
            List<Element> elements = new List<Element>();
            //bool retVal = false;
            try
            {
                elements = factory.ListAllElements(name);
                //retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return elements;
        }


    }
}
