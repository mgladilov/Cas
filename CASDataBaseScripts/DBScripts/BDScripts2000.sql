if not exists ( select  *
			from    sys.columns c                        
			where   c.object_id = object_id('Dictionaries.Departments')
					and c.name = 'Address' ) 

	alter table Dictionaries.Departments
	add Address nvarchar (256) 
GO

if not exists ( select  *
			from    sys.columns c                        
			where   c.object_id = object_id('Dictionaries.Departments')
					and c.name = 'Phone' ) 

	alter table Dictionaries.Departments
	add Phone nvarchar (256) 
GO

if not exists ( select  *
			from    sys.columns c                        
			where   c.object_id = object_id('Dictionaries.Departments')
					and c.name = 'Fax' ) 

	alter table Dictionaries.Departments
	add Fax nvarchar (256) 
GO

if not exists ( select  *
			from    sys.columns c                        
			where   c.object_id = object_id('Dictionaries.Departments')
					and c.name = 'Email' ) 

	alter table Dictionaries.Departments
	add Email nvarchar (256) 
GO

if not exists ( select  *
			from    sys.columns c                        
			where   c.object_id = object_id('Dictionaries.Departments')
					and c.name = 'Website' ) 

	alter table Dictionaries.Departments
	add Website nvarchar (256) 
GO

if not exists ( select  *
			from    sys.columns c                        
			where   c.object_id = object_id('dbo.InitionalOrderRecords')
					and c.name = 'AirportCodeId' ) 

	alter table dbo.InitionalOrderRecords
	add AirportCodeId int not null default -1 
GO

if not exists ( select  *
			from    sys.columns c                        
			where   c.object_id = object_id('dbo.InitionalOrderRecords')
					and c.name = 'Reference' ) 

	alter table dbo.InitionalOrderRecords
	add Reference nvarchar (MAX)
GO

---------------------------------------------------------------------------

if exists ( select  *
			from    sys.columns c                        
			where   c.object_id = object_id('dbo.PurchaseOrders')
					and c.name = 'ShipTo' ) 

	alter table dbo.PurchaseOrders
	drop column ShipTo 
GO


if not exists ( select  *
			from    sys.columns c                        
			where   c.object_id = object_id('dbo.PurchaseOrders')
					and c.name = 'ShipToId' ) 

	alter table dbo.PurchaseOrders
	add ShipToId int not null default -1
GO

if not exists ( select  *
			from    sys.columns c                        
			where   c.object_id = object_id('dbo.PurchaseOrders')
					and c.name = 'Net' ) 

	alter table dbo.PurchaseOrders
	add Net float not null default 0
GO

if not exists ( select  *
			from    sys.columns c                        
			where   c.object_id = object_id('dbo.PurchaseOrders')
					and c.name = 'IncoTermRef' ) 

	alter table dbo.PurchaseOrders
	add IncoTermRef nvarchar(MAX)
GO

if not exists ( select  *
			from    sys.columns c                        
			where   c.object_id = object_id('dbo.PurchaseOrders')
					and c.name = 'StationId' ) 

	alter table dbo.PurchaseOrders
	add StationId int not null default -1
GO

if not exists ( select  *
			from    sys.columns c                        
			where   c.object_id = object_id('dbo.PurchaseOrders')
					and c.name = 'TrackingNo' ) 

	alter table dbo.PurchaseOrders
	add TrackingNo nvarchar(MAX)
GO