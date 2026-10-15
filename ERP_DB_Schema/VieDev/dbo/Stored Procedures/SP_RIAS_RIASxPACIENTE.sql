
-- =============================================
-- Author:		Rafael Eduardo Patiño
-- Create date: 18/11/2018
-- Description:	retorna las RIAS que apliquen para la edad del paciente
-- =============================================
CREATE PROCEDURE [dbo].[SP_RIAS_RIASxPACIENTE]
(
  @Identificacion as varchar(25)
)
AS
BEGIN
  SET NOCOUNT ON;
        
     declare @Sexo as integer
	 declare @FechaNacimiento as datetime
	 declare @TableRIAS table(ID INTEGER NOT NULL,    CODIGO varchar(5) NOT NULL, NOMBRE varchar(150) NOT NULL)

	 select @Sexo = IPSEXOPAC,@FechaNacimiento = IPFECNACI from INPACIENT where IPCODPACI = @Identificacion	 
	 declare @EdadAnos as integer = datediff (YEAR  , @FechaNacimiento , [Common].[GETDATE]() ) 
	 declare @EdadMeses as integer= datediff (MONTH , @FechaNacimiento,[Common].[GETDATE]() ) 
	 declare @Edaddias as integer= datediff (DAY , @FechaNacimiento,[Common].[GETDATE]() ) 	 
	 
	 --Rias aplican x años
	 insert into @TableRIAS 
	 select ID,CODPRO,NOMBRE  from RIAS R  where (@EdadAnos BETWEEN EDADMINIMA AND EDADMAXIMA ) AND TIPOEDAD = 2 AND SEXO IN(@Sexo,3) AND ESTADO = 1	 

	 --Rias aplican x meses
	 insert into @TableRIAS 
	 select ID,CODPRO,NOMBRE from RIAS R  where (@EdadMeses BETWEEN EDADMINIMA AND EDADMAXIMA ) AND TIPOEDAD = 1 AND SEXO IN(@Sexo,3) AND ESTADO = 1	 	 

	 --Rias aplican x dias
	 insert into @TableRIAS 
	 select ID,CODPRO,NOMBRE from RIAS R  where (@Edaddias BETWEEN EDADMINIMA AND EDADMAXIMA ) AND TIPOEDAD = 0 AND SEXO IN(@Sexo,3) AND ESTADO = 1
	 
	 --se listan solo las Rias a las que no esta ya inscrito el paciente
	 select ID,CODIGO AS 'Codigo',NOMBRE as 'Descripcion' from @TableRIAS where ID not in (select IDRIAS from RIASXPACIENTE where IPCODPACI = @Identificacion AND ESTADO = 2 )
	   
END