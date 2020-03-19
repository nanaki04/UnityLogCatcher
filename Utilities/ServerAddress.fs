namespace UnityLogCatcher.Utilities

type ServerAddress = {
  Ip : IpAddress;
  Port : Port;
}

module ServerAddress =
  let empty = {
    Ip = IpAddress.empty;
    Port = Port.empty;
  }

  let withIp ipAddress serverAddress =
    { serverAddress with Ip = ipAddress }

  let withPort port serverAddress =
    { serverAddress with Port = port }

  let ip serverAddress =
    serverAddress.Ip

  let port serverAddress =
    serverAddress.Port
