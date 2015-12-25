# CacheFramework
Please follow the instruction below:

1) Download and install PostSharp in order to enable the AOP (free version from https://www.postsharp.net/)
2) Download and Install NCache Express (NCache Open Source (64-bit) (.msi) (FREE) from http://www.alachisoft.com/download-ncache.html)
3) Make sure the nuget packets are installed and have the same dll versions in all projects
4) Ninject is used as a dependency injection tool (if its not installed install the packages but might not be nececery)
5) Download and install AppFabric in case you need that cache (https://www.microsoft.com/en-us/download/details.aspx?id=27115)

********************************************* NCache Configuration ************************************************************
	
	1)	Create the cache on the desired server
			  CreateCache MyCacheName /s ServerIPAddress   												                                -- topology type: local cache
			  CreateCache MyCacheName.Partitioned /s ServerIPAddress /C ClusterPort /S CacheSize /t partitioned		-- topology type: partitioned cache
			  CreateCache MyCacheName /s ServerIPAddress /t replicated /C ClusterPort   				                	-- topology type: replicated cache
	2)  List Cache
	      ListCaches /a
	3)  Clear Cache
	      ClearCache MyCacheName
	4)  Start Cache
	      StartCache MyCacheName
	5)	If port is taken release it
			netstat -aon |find /i "listening" |find "PortNo"  										                          		-- find particular port
			taskkill.exe /pid PidNo /f                       											                          	  -- kill task that uses that port
	6)	Start the cache on the desired server	
			StartCache MyCacheName /s ServerIPAddress /p PortNo                                                 --default port 8250
			
********************************************************************************************************************************

******************************************** AppFabric Configuration ***********************************************************
	
	1)	Create the cache on the desired server
			New-Cache -CacheName NamedCache1 -Secondaries 1 -TimeToLive 15
	2)  Remove Cache 
	    Remove-Cache -CacheName NamedCache1
	3)  Restart Cache
	    Restart-CacheCluster
			
********************************************************************************************************************************
