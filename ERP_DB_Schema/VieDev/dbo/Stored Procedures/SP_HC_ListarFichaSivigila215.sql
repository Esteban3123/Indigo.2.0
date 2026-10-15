-- Stored Procedure
-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,16-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila215]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON; 
		
			Select
				Rtrim(NOMMAD) As 'NOMMAD', 
				CASE Rtrim(TIPID)  WHEN '1' THEN 'RC - Registro Civil' WHEN '2' THEN 'TI - Tarjeta de Identidad' WHEN '3' THEN 'CC - Cédula de Ciudadanía' WHEN '4' THEN 'CE - Cédula de Extranjería' WHEN '5' THEN 'PA - Pasaporte' WHEN '6' THEN 'MS - Menor Sin Identificación' WHEN '7' THEN 'AS - Adulto Sin Identificación' WHEN '8' THEN 'PE - Permiso Especial de Permanencia' WHEN '9' THEN 'CN - Certificado de Nacido Vivo' WHEN 10 THEN 'PT - Permiso Temporal de Permanencia' END AS 'TIPID', 				
				Rtrim(NUMIDENT) As 'NUMIDENT', Rtrim(EDAD) As 'EDAD',
				Rtrim(NUMEMB) As 'NUMEMB', Rtrim(NACVIVOS) As 'NACVIVOS', Rtrim(ABORTOS) As 'ABORTOS', Rtrim(MORTINAT) As 'MORTINAT',
				CASE DIAGNOS WHEN '1' THEN 'X' END AS 'DIAGNOS Si',CASE DIAGNOS WHEN '0' THEN 'X' END AS 'DIAGNOS No', Rtrim(EDADGESTDIA) As 'EDADGESTDIA',
				CASE PATCRON WHEN '1' THEN 'X' END AS 'PATCRON Si',CASE PATCRON WHEN '0' THEN 'X' END AS 'PATCRON No', Rtrim(CUALES) As 'CUALES',
				CASE EMBMULT WHEN '1' THEN 'X' END AS 'EMBMULT Si',CASE EMBMULT WHEN '0' THEN 'X' END AS 'EMBMULT No',
				CASE NATIVIVO WHEN '1' THEN 'X' END AS 'si', CASE NATIVIVO WHEN '2' THEN 'X' END AS 'no', CASE NATIVIVO WHEN '3' THEN 'X' END AS 'No ha nacido',
				Rtrim(EDADGESTNAC) As 'EDADGESTNAC', Rtrim(PESO) As 'PESO', Rtrim(PERIMCEF) As 'PERIMCEF',
				Rtrim(DESC1) As 'DESC1', Rtrim(DESC2) As 'DESC2', Rtrim(DESC3) As 'DESC3', Rtrim(DESC4) As 'DESC4',
				Rtrim(DESC5) As 'DESC5', Rtrim(DESC6) As 'DESC6', Rtrim(DESC7) As 'DESC7', Rtrim(DESC8) As 'DESC8',
				CASE STORCH WHEN '1' THEN 'X' END AS 'STORCH Si',CASE STORCH WHEN '0' THEN 'X' END AS 'STORCH No',
				CASE TSH1 WHEN '1' THEN 'X' END AS 'TSH1 Si',CASE TSH1 WHEN '0' THEN 'X' END AS 'TSH1 No',
				CASE TOTSUE1 WHEN '1' THEN 'X' END AS 'TOTSUE1 Si',CASE TOTSUE1 WHEN '0' THEN 'X' END AS 'TOTSUE1 No',
				CASE LIBSUE1 WHEN '1' THEN 'X' END AS 'LIBSUE1 Si',CASE LIBSUE1 WHEN '0' THEN 'X' END AS 'LIBSUE1 No',
				CASE TSH2 WHEN '1' THEN 'X' END AS 'TSH2 Si',CASE TSH2 WHEN '0' THEN 'X' END AS 'TSH2 No',
				CASE TOTSUE2 WHEN '1' THEN 'X' END AS 'TOTSUE2 Si',CASE TOTSUE2 WHEN '0' THEN 'X' END AS 'TOTSUE2 No',
				CASE LIBSUE2 WHEN '1' THEN 'X' END AS 'LIBSUE2 Si',CASE LIBSUE2 WHEN '0' THEN 'X' END AS 'LIBSUE2 No', 
				VERSION AS 'VERSION', 
				JSON AS 'JSON' 

			From 
				HCFICHA215 

			Where 
				IDFICHANOTIFICACION  = @IdFicha 

END