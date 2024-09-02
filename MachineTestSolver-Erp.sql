Create DataBase Db_Test_Core   --create database
go

use Db_Test_Core --use data base
go

CREATE TABLE [dbo].[Customer]( --created Customer Table
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerCode] [varchar](100) NULL Unique,
	[Name] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
	[MobileNo] [varchar](15) NULL,
	[VisitedDate] [datetime] NULL)

	GO

CREATE TABLE [dbo].[CustomerAddress]( --created [CustomerAddress] Table for store customer address
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NULL,
	[AddressLine1] [varchar](100) NULL,
	[AddressLine2] [varchar](100) NULL,
	[GeoLocation] [varchar](100) NULL,
	[AddressType] [varchar](100) NULL
) ON [PRIMARY]
GO

Create PROCEDURE [dbo].[SpCreateCustomer] --SP for Customer creation
(
    @Name varchar(100),
    @Email nvarchar(100),
    @MobileNo nvarchar(100),
    @VisitedDate Datetime,
    @CustomerAddressListData varchar(8000),
    @CustomerDataBack varchar(8000) OUT,
    @CustomerAddressListDataBack varchar(8000) OUT
)
AS 
BEGIN
    DECLARE @CustomerId int;

    INSERT INTO Customer (Name, Email, MobileNo,VisitedDate)
    VALUES (@Name, @Email, @MobileNo,@VisitedDate);

 
    SET @CustomerId = SCOPE_IDENTITY();

   
    UPDATE Customer 
    SET CustomerCode = 'CUSTO00' + CAST(@CustomerId AS varchar(10)) 
    WHERE ID = @CustomerId;

   
    IF (@CustomerAddressListData IS NOT NULL AND LEN(@CustomerAddressListData) > 0)
    BEGIN
        INSERT INTO CustomerAddress (CustomerId, AddressLine1, AddressLine2, GeoLocation,AddressType)
        SELECT 
            @CustomerId,
            JSON_VALUE(value, '$.AddressLine1') AS AddressLine1,
            JSON_VALUE(value, '$.AddressLine2') AS AddressLine2,
            JSON_VALUE(value, '$.GeoLocation') AS GeoLocation,
			JSON_VALUE(value, '$.AddressType') AS AddressType
        FROM OPENJSON(@CustomerAddressListData);
    SELECT @CustomerDataBack = 
    (SELECT * FROM Customer WHERE Id = @CustomerId FOR JSON Path);

    SELECT @CustomerAddressListDataBack = 
    (SELECT * FROM CustomerAddress WHERE CustomerId = @CustomerId FOR JSON AUTO);
    END
END;



ALTER PROCEDURE [dbo].[SpFindCustomer]
(
    @CustomerId INT,
    @CustomerDataBack VARCHAR(MAX) OUT,
    @CustomerAddressListDataBack VARCHAR(MAX) OUT
)
AS
BEGIN
    
    SET @CustomerDataBack = '';
    SET @CustomerAddressListDataBack = '';

   
    IF EXISTS (SELECT 1 FROM Customer WHERE Id = @CustomerId)
    BEGIN
        
        SELECT @CustomerDataBack = 
            (SELECT * FROM Customer WHERE Id = @CustomerId FOR JSON PATH, INCLUDE_NULL_VALUES);

        SELECT @CustomerAddressListDataBack = 
            (SELECT * FROM CustomerAddress WHERE CustomerId = @CustomerId FOR JSON AUTO, INCLUDE_NULL_VALUES);
    END
    ELSE
    BEGIN
         RAISERROR('Customer with ID %d not found.', 16, 1, @CustomerId);
        SET @CustomerDataBack = '';
        SET @CustomerAddressListDataBack = '[]'; 
    END
END;

create  PROCEDURE [dbo].[SpUpdateCustomer] --sp for Update customer
(
    @CustomerId INT,
    @Name VARCHAR(100),
    @Email NVARCHAR(100),
    @MobileNo NVARCHAR(100),
    @VisitedDate DATETIME,
    @CustomerAddressListData VARCHAR(8000),
    @CustomerDataBack VARCHAR(8000) OUT,
    @CustomerAddressListDataBack VARCHAR(8000) OUT
)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Customer WHERE Id = @CustomerId)
    BEGIN
    UPDATE Customer
    SET
        Name = @Name,
        Email = @Email,
        MobileNo = @MobileNo,
        VisitedDate = @VisitedDate
    WHERE Id = @CustomerId;

   
    UPDATE Customer
    SET CustomerCode = 'CUSTO00' + CAST(@CustomerId AS VARCHAR(10))
    WHERE Id = @CustomerId;

    IF (@CustomerAddressListData IS NOT NULL AND LEN(@CustomerAddressListData) > 0)
    BEGIN
     
        DELETE FROM CustomerAddress
        WHERE CustomerId = @CustomerId;

    
        INSERT INTO CustomerAddress (CustomerId, AddressLine1, AddressLine2, GeoLocation, AddressType)
        SELECT 
            @CustomerId,
            JSON_VALUE(value, '$.AddressLine1') AS AddressLine1,
            JSON_VALUE(value, '$.AddressLine2') AS AddressLine2,
            JSON_VALUE(value, '$.GeoLocation') AS GeoLocation,
            JSON_VALUE(value, '$.AddressType') AS AddressType
        FROM OPENJSON(@CustomerAddressListData);
    END

    -- Return updated customer data as JSON
    SELECT @CustomerDataBack = 
        (SELECT * FROM Customer WHERE Id = @CustomerId FOR JSON PATH);

    -- Return updated customer addresses as JSON
    SELECT @CustomerAddressListDataBack = 
        (SELECT * FROM CustomerAddress WHERE CustomerId = @CustomerId FOR JSON AUTO);
	end
	else
		begin
			 RAISERROR('Customer with ID %d not found.', 16, 1, @CustomerId);
		end
END;

alter PROCEDURE [dbo].[SpDeleteCustomer] --sp for delete customer
(
    @CustomerId INT,
    @DeletedCustomerData VARCHAR(MAX) OUT,
    @DeletedCustomerAddressListData VARCHAR(MAX) OUT
)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Customer WHERE Id = @CustomerId)
    BEGIN
    DECLARE @DeletedCustomerDataTemp TABLE (Id INT,CustomerCode VARCHAR(20), Name VARCHAR(100), Email NVARCHAR(100), MobileNo NVARCHAR(100), VisitedDate DATETIME);
    DECLARE @DeletedCustomerAddressListDataTemp TABLE (Id int,CustomerId INT, AddressLine1 VARCHAR(255), AddressLine2 VARCHAR(255), GeoLocation VARCHAR(50), AddressType VARCHAR(50));


    INSERT INTO @DeletedCustomerDataTemp
    SELECT * FROM Customer WHERE Id = @CustomerId;

   
    INSERT INTO @DeletedCustomerAddressListDataTemp
    SELECT * FROM CustomerAddress WHERE CustomerId = @CustomerId;

    
    DELETE FROM Customer WHERE Id = @CustomerId;
    DELETE FROM CustomerAddress WHERE CustomerId = @CustomerId;

    
    SELECT @DeletedCustomerData = 
        (SELECT * FROM @DeletedCustomerDataTemp FOR JSON PATH, INCLUDE_NULL_VALUES);

    
    SELECT @DeletedCustomerAddressListData = 
        (SELECT * FROM @DeletedCustomerAddressListDataTemp FOR JSON AUTO, INCLUDE_NULL_VALUES);
	end
	else
	begin
		RAISERROR('Customer with ID %d not found.', 16, 1, @CustomerId);
	end
END;
ALTER PROCEDURE [dbo].[SpGetCustomers] --spget customer list with search filter
(
    @Name VARCHAR(100) = NULL,
    @Email NVARCHAR(100) = NULL,
    @MobileNo NVARCHAR(100) = NULL,
    @VisitedDateFrom DATETIME = NULL,
    @VisitedDateTo DATETIME = NULL
)
AS
BEGIN
    
    DECLARE @SQL NVARCHAR(MAX);
    SET @SQL = N'SELECT * FROM Customer WHERE 1=1';

   
    IF @Name IS NOT NULL
    BEGIN
        SET @SQL = @SQL + ' AND Name LIKE ''%' + @Name + '%''';
    END

    IF @Email IS NOT NULL
    BEGIN
        SET @SQL = @SQL + ' AND Name LIKE ''%' + @Email + '%''';
    END

    IF @MobileNo IS NOT NULL
    BEGIN
        SET @SQL = @SQL + ' AND Name LIKE ''%' + @MobileNo + '%''';
    END

    IF @VisitedDateFrom IS NOT NULL
    BEGIN
        SET @SQL = @SQL + ' AND VisitedDate >= @VisitedDateFrom';
    END

    IF @VisitedDateTo IS NOT NULL
    BEGIN
        SET @SQL = @SQL + ' AND VisitedDate <= @VisitedDateTo';
    END

    -- Execute the dynamic SQL query
    EXEC sp_executesql @SQL,
        N'@Name VARCHAR(100), @Email NVARCHAR(100), @MobileNo NVARCHAR(100), @VisitedDateFrom DATETIME, @VisitedDateTo DATETIME',
        @Name = @Name, 
        @Email = @Email, 
        @MobileNo = @MobileNo, 
        @VisitedDateFrom = @VisitedDateFrom, 
        @VisitedDateTo = @VisitedDateTo;
END;