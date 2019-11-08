using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace signarl114.Controllers
{
    //采用持久化连接类（PersistentConnection）
    public class MyConnection1 : PersistentConnection
    {
        private static List<string> monitoringIdList = new List<string>();
        //mm小笔记：
        //onConnected :代表客户服务的创建， OnReceived：代表处理其他客户服务send过来的东西
        //Connection.Send(connectionId, "ready"); 给谁发 发什么 （可以群发）
        protected override Task OnConnected(IRequest request, string connectionId)
        {
            //从request中取出相应的参数
            bool IsMonitoring = (request.QueryString["Monitoring"] ?? "").ToString() == "Y";
            if (IsMonitoring)
            {
                if (!monitoringIdList.Contains(connectionId))
                {
                    monitoringIdList.Add(connectionId);
                }
                return Connection.Send(connectionId, "ready");
            }
            else
            {
                if (monitoringIdList.Count > 0)
                {
                    return Connection.Send(monitoringIdList, "in_" + connectionId);
                }
                else
                {
                    return Connection.Send(connectionId, "nobody");
                }
            }
        }

        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            if (monitoringIdList.Contains(connectionId))
            {
                return Connection.Send(data, "通过了");
            }
            return null;
        }

        protected override Task OnDisconnected(IRequest request, string connectionId, bool stopCalled)
        {
            if (!monitoringIdList.Contains(connectionId))
            {
                return Connection.Send(monitoringIdList, "out_" + connectionId);
            }
            return null;
        }
    }
}