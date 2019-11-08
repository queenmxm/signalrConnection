# signalrConnection
signalrconnection持久连接
 耗时的操作最好用websocket（比如 支付 状态的校验 和  批量数据的发送）

1.signrl使用时机：点击支付的时候 ：生成二维码，前端开始给服务器发送查询支付状态请求 conn.send(orderID);  这个是由后端的onreceive方法来接，后端的send方法 使用前端来接  
谁调用方法 那么connectid就是谁的，如果想发给别人，就传别人的数据 ，再从后台发给这个人。
