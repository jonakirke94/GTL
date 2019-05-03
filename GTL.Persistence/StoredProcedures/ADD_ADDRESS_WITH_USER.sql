USE GTL
GO

CREATE PROCEDURE dbo.ADD_ADDRESS_WITH_USER @memberssn nvarchar(15), @streetname varchar(50), @housenumber varchar(10), @zipcode varchar(8), @type varchar(20), @city varchar(30)
AS
BEGIN
DECLARE @address_id INTEGER;
INSERT INTO Address(StreetName, HouseNumber, ZipCode, City, Type)
VALUES(@streetname, @housenumber, @zipcode, @city, @type)
SET @address_id = SCOPE_IDENTITY()
INSERT INTO MemberAddress(MemberSsn, AddressId)
VALUES(@memberssn, @address_id);
END