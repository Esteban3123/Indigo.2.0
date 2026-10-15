-- =============================================
-- Author:		Rafael PAtiño
-- Create date: 16/07/2019
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SubirSolicutdesFisico]
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	
	DECLARE @Tabla AS TABLE(id INT IDENTITY,IPCODPACI varchar(100),NUMINGRES VARCHAR(50),CODCENATE VARCHAR(50), UFUCODIGO VARCHAR(50), CODPRODUC varchar(100),TIPPRODUC int,CANENTPRO int,CANPEDPRO int)

	INSERT INTO @Tabla
	select D.IPCODPACI,D.NUMINGRES,D.CODCENATE,D.UFUCODIGO, D.CODPRODUC, P.TIPPRODUC,D.CANPEDPRO  as cantidadPedida,D.CANENTPRO as cantidadEntregada    from HCFARMEPD D inner join HCFARMEPC C on C.CODCONCEC = D.CODCONCEC 
inner join IHLISTPRO P on P.CODPRODUC = D.CODPRODUC 
where C.ORDESTADO  =3 and D.PROESTADO = 2 and D.IDAGEPROGQX is null and format(C.FECHAORDE,'dd/MM/yyyy HH:mm') >= '16/07/2019 09:50' and format(C.FECHAORDE,'dd/MM/yyyy HH:mm') <= '16/07/2019 13:15'


	DECLARE @count AS INTEGER =  (SELECT COUNT(*) FROM @Tabla)
	DECLARE @contador AS INT = 1

	PRINT 'CANTIDA REG PRO: ' + CONVERT(VARCHAR(20),@count)

	WHILE @contador  <= @count begin
			
		declare @PatientCode varchar(25)
		declare @AdmissionNumberHijo varchar(100)
		declare @CareCenterCode varchar(100)
		declare @FunctionalUnitCode varchar(100)
		declare @ProductCodeCrystal varchar(100)
		declare @QuantityCrystal  int
		declare @ProductTypeCrystal int
		declare @CantidadSolicitadaCrystal int


		select  @PatientCode = IPCODPACI, @AdmissionNumberHijo = NUMINGRES , @CareCenterCode = CODCENATE,@FunctionalUnitCode = UFUCODIGO, @ProductCodeCrystal = CODPRODUC 
				, @QuantityCrystal = CANENTPRO, @ProductTypeCrystal = TIPPRODUC, @CantidadSolicitadaCrystal = CANPEDPRO   
		    from @Tabla WHERE ID = @contador


		if Exists (select IPCODPACI from dbo.HCFISIPRO 
										where IPCODPACI = @PatientCode and NUMINGRES = @AdmissionNumberHijo and CODCENATE = @CareCenterCode 
											And UFUCODIGO = @FunctionalUnitCode and CODPRODUC = @ProductCodeCrystal) BEGIN

											
										Update dbo.HCFISIPRO set CANACTPRO += @QuantityCrystal 
										Where IPCODPACI = @PatientCode and NUMINGRES = @AdmissionNumberHijo and CODCENATE = @CareCenterCode 
											And UFUCODIGO = @FunctionalUnitCode and CODPRODUC = @ProductCodeCrystal  -- select 11

				end else begin


								INSERT INTO [dbo].[HCFISIPRO]
										   ([IPCODPACI]
										   ,[NUMINGRES]
										   ,[CODCENATE]
										   ,[UFUCODIGO]
										   ,[CODPRODUC]
										   ,[TIPPRODUC]
										   ,[CANACTPRO]
										   ,[CANPEDPRO]
										   ,[CANPENPRO]
										   ,[TOTPROUNI]
										   ,[DOSPROACU]
										   ,[TOTHORACU]
										   ,[CODUNIMED]
										   ,[INDAUDFOR])
									 VALUES
										   (@PatientCode
										   ,@AdmissionNumberHijo
										   ,@CareCenterCode
										   ,@FunctionalUnitCode
										   ,@ProductCodeCrystal
										   ,@ProductTypeCrystal
										   ,@QuantityCrystal
										   ,@CantidadSolicitadaCrystal
										   ,@CantidadSolicitadaCrystal - @QuantityCrystal
										   ,NULL
										   ,NULL
										   ,NULL
										   ,NULL
										   ,0) 
										   --select 22


					end

	



			   PRINT 'CONTEO: ' + CONVERT(VARCHAR(20),@contador)
		SET @contador +=1 
	END

    
END