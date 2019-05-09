USE [GTL]
GO

INSERT INTO [dbo].[Staff]
           ([Name]
           ,[Email]
           ,[Role]
           ,[PasswordHash]
           ,[PasswordSalt]
           ,[PasswordLastChanged])
     VALUES
           ('Thanos'
           ,'test@gtl.dk'
           ,'CHIEFLIBRARIAN'
           ,'epPFRg1BkKiVHU5qHBwSoFiJTqG0MluHrsyMaHU9jWs='
           ,'QuaS9syLoW9Ap0spnq9ypw=='
           ,GETDATE())

		   --password 12345678
GO


