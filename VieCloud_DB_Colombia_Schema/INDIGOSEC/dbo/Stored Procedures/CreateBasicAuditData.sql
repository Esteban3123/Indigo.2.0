
-- =======================================================
-- Author:		Juan Diego Diaz Mosquera
-- Create date: 31/10/2013
-- Description:	Sp para crear dinamicamente la data de auditoria basica
-- =======================================================
CREATE PROCEDURE [dbo].[CreateBasicAuditData]
	@Entity as varchar(100),
	@Form as int,
	@RegisterId as varchar(20),
	@UserName as varchar(200),
	@UserCode as varchar(20),
	@UserMachine as varchar(50) = '',
	@Parameters as varchar(100) = '',
	@ReportName as varchar(50) = '',
	@Operation as tinyint,
	@DateAudit as datetime,
	@Company as varchar(2),
	@ContainerSecurity as varchar(40) = '',
	@Count as int
AS
BEGIN
SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN

		DECLARE @cnt INT = 0;

		WHILE @cnt < @Count
		BEGIN
		   INSERT INTO [Audit].[BasicAudit]
            (Tag
           ,Entity
           ,RegisterId
           ,UserCode
           ,UserName
		   ,UserMachine
		   ,Parameters
		   ,ReportName
           ,TransactionDate
           ,Operation
		   ,Company)
		   VALUES
			(@Form,@Entity,@RegisterId,@UserCode,@UserName,@UserMachine,@Parameters,@ReportName,@DateAudit,@Operation, @Company)

		   SET @cnt = @cnt + 1;
		END;
	COMMIT TRAN

	SELECT 'Se ha guardado satisfactoriamente' as Mensaje,1 as CodigoMensaje,'true' as Estado

	END TRY
	BEGIN CATCH
	  ROLLBACK TRAN
		 SELECT  'Se ha producido un error: '+  ERROR_MESSAGE()	as Mensaje, ERROR_NUMBER() as CodigoMensaje,'false' as Estado
	END CATCH

END
