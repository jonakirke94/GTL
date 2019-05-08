USE [GTL]
GO
/****** Object:  Trigger [dbo].[CHECKLOANABLE]    Script Date: 05-05-2019 17:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[CHECK_LOANABLE]
ON [dbo].[Loan]
AFTER INSERT
AS BEGIN
SET NOCOUNT ON;
DECLARE @Status varchar(20)
SELECT @Status = Status FROM Copy JOIN Loan ON Copy.Barcode = Loan.CopyBarcode WHERE Barcode = CopyBarcode

IF @Status <> 'AVAILABLE' 
BEGIN
	RAISERROR (N'This copy is not avilable to be lend out. Status: %s', 16, 1, @Status);
	ROLLBACK TRANSACTION;
	RETURN 
END;

END;
