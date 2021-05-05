# Port forwarding

Port forwarding is an alternative to using hamachi and is only needed to be done by the person hosting the server. You may find this to be more complicated than using hamachi as it requires editing your router's settings. If you cannot access your router's settings page or don't know your routers settings password then you cannot use this method. In this case then you should use a method such as [hamachi](https://rarisma.github.io/Simple-Server-Manager/help/connections/general/hamachi)


# How to setup port forwarding
- To begin with you will need to have access to your router page you can usually do this by opening your browser and going to going to 192.168.0.1 (depending on where you live this may be different)
- You may be asked for a password, this can be found on your router however it may have been changed if you live with other people
- You will now want to find the local IP [by following this guide here](https://support.microsoft.com/en-us/windows/find-your-ip-address-f21a9bbc-c582-55cd-35e0-73431160a1b9)
- Now you can start port forwarding, look for this option in your router page, you may have to enable an expert/advanced mode in your router if it has one or it may be under security or firewall options
- Now create a new portforwarding rule, enter your local ip and the ports SSM tells you to follow
- Once done, double check and then click apply/save



# FAQ
## How hard is port forwarding / I don't understand your guide
Port forwarding isn't black magic.
Its fairly easy to do if you have a good guide, if this guide didn't work then you might have more luck searching for a guide for your spesific router.

## How do people connect when I've set up port forwarding
Simply send the person who wants to connect your public IP (Search What is my IP and it will tell your public IP)

## Is port forwarding safe
port forwarding is safe aslong as you keep your devices up to date, however when you are finished hosting the server for good then you should try to remember to close the port.

## Is port forwarding better than hamachi
definitely, and in some cases will fix problems caused by hamachi, such as blocked/relayed tunnels, it may also reduce latency in some cases. So you should try port forwarding if you feel competent enough.
