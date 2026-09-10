CREATE PROCEDURE [dbo].[SP_ONCO_GenerarHistoricoCAC]

AS
BEGIN
  SET NOCOUNT ON;

  BEGIN TRANSACTION TRANSACCIONHISTORICOCAC;  
  
	BEGIN TRY  

	 --1 ya que se debe ejeuctar el 01/01/2025 11:59 para que quede el historico del 2024
	declare @Anio as integer = year(common.Getdate()) - 1
 
	---Se crea tabla con los datos a corte del año 2024
	Drop table IF EXISTS HCONCOPREG_DATOS_2024
	Select * Into HCONCOPREG_DATOS_2024  from  HCONCOPREG 

	--Insertamos Informacion en tabla Historica
    delete from HCONCOPREGHISTORICO where ANIO = @Anio
	INSERT INTO [dbo].[HCONCOPREGHISTORICO]
	SELECT [Id],[CODDIAGNO],[IDEntidadVIE],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31],[32],[33],[34],[35],[36],NULL as CONFIRMARESTADIO36 ,[37],[38],NULL as CONFIRMARESTADIO38,[39],[40],[41],[42],[43],[44],[45],[46],[46.1],[46.2],[46.3],[46.4],[46.5],[46.6],[46.7],[46.8],[47],[48],[49],[50],[51],[52],[53],[53.1],[53.2],[53.3],[53.4],[53.5],[53.6],[53.7],[53.8],[53.9],[54],[55],[56],[57],[58],[59],[60],[61],[62],[63],[64],[65],[66],[66.1],[66.2],[66.3],[66.4],[66.5],[66.6],[66.7],[66.8],[66.9],[67],[68],[69],[70],[71],[72],[73],[74],[75],[76],[77],[78],[79],[80],[81],[82],[83],[84],[85],[86],[87],[88],[89],[90],[91],[92],[93],[94],[95],[96],[97],[98],[99],[100],[101],[102],[103],[104],[105],[106],[107],[108],[109],[110],[111],[112],[113],[114],[114.1],[114.2],[114.3],[114.4],[114.5],[114.6],[115],[116],[117],[118],[119],[120],[121],[122],[123],[124],[125],[126],[127],[128],[129],[130],[131],[132],[133],[134],[AdicionalCAC1],[AdicionalCAC2],[AdicionalCAC3],[AdicionalCAC4],[AdicionalCAC5],[AdicionalCAC6],[AdicionalCAC7],[AdicionalCAC8],[AdicionalCAC9],[AdicionalCAC10],[AdicionalCAC11],[AdicionalCAC12],[AdicionalCAC13],[AdicionalCAC14],[AdicionalCAC16],[AdicionalCAC17],[AdicionalCAC18],[AdicionalCAC19],[AdicionalCAC20],[AdicionalCAC21],[AdicionalCAC25],[AdicionalCAC26],[AdicionalCAC27],[AdicionalCAC28],[AdicionalCAC29],[AdicionalCAC30],[AdicionalCAC31],[AdicionalCAC32],[AdicionalCAC33],[AdicionalCAC34],[AdicionalCAC35],[AdicionalODO1],[AdicionalODO2],[AdicionalODO3],[AdicionalODO4],[AdicionalODO5],[AdicionalODO6],[AdicionalODO7],[AdicionalODO8],[AdicionalODO9],[AdicionalODO10],[AdicionalODO11],NULL as IDHCORDQUIMIO53_1,null as IDHCORDQUIMIO66_1,@Anio, /*CAST('31-12-2021 11:59' AS datetime) */ Common.GETDATE() AS 'FECHAREGISTRO' 
	FROM HCONCOPREG

	--Se elimina los pacientes marcados como Fallecidos = 2
	DELETE FROM HCONCOPREG WHERE [127] = '2'

	--Se hace el reset de las variables de CAC
	UPDATE HCONCOPREG
	SET  [45] = '98',[46] = '98',[46.1] = 97 ,[46.2]= 97,[46.3]= 97,[46.4]= 97,[46.5]= 97,[46.6]= 97,[46.7]= 97,[46.8]= 97,
	[47] = '98', [48] = '98', [49] = '1845-01-01', [50] = '98', [51] = '98', [52] = '98', [53] = '98', [53.1] = '98', [53.2] = '98', [53.3] = '98', [53.4] = '98',
	[53.5] = '98', [53.6] = '98', [53.7] = '98', [53.8] = '98', [53.9] = '98', [54] = '98' , [55] = '98', [56] = '98', [57] = '98', [58] = '1845-01-01', [59] = '98', [60] = '98' , [61] = '98',
	[62] = '1845-01-01', [63] = '98', [64] = '98', [65] = '98', [66] = '98' , [66.1] = '98', [66.2] = '98', [66.3] = '98', [66.4] = '98', [66.5] = '98', [66.6] = '98' , [66.7] = '98', [66.8] = '98',
	[66.9] = '98', [67] = '98', [68] = '98', [69] = '98' , [70] = '98', [71] = '1845-01-01', [72] = '98', [73] = '98', [74] = '2', [75] = '98', [76] = '1845-01-01', [77] = '98', [78] = '98' ,
	[79] = '98', [80] = '1845-01-01', [81] = '98', [82] = '98', [83] = '98', [84] = '98', [85] = '98' , [86] = '98', [87] = '98', [88] = '1845-01-01', [89] = '98', [90] = '98', [91] = '98' , 
	[92] = '98', [93] = '98', [94] = '1845-01-01', [95] = '98', [96] = '98', [97] = '1845-01-01' , [98] = '98', [99] = '98', [100] = '98', [101] = '98', [102] = '98', [103] = '1845-01-01' , 
	[104] = '98', [105] = '98', [106] = '98', [107] = '98', [108] = '98', [109] = '1845-01-01' , [110] = '98', [111] = '98', [112] = '1845-01-01', [113] = '98', [114] = '2', [114.1] = '2' , 
	[114.2] = '2', [114.3] = '2', [114.4] = '2', [114.5] = '2', [114.6] = '2', [115] = '1845-01-01', [116] = '98', [117] = '98', [118] = '1845-01-01', [119] = '98', [120] = '98', [121] = '1845-01-01',
	[122] = '98', [123] = '4', [124] = '98', [128] = '0', [130] = '1845-01-01', [131] = '1845-01-01', [132] = '98', [AdicionalCAC13] = '0', [AdicionalCAC14] = '0', [AdicionalCAC16] = '0', [AdicionalCAC25] = '98'

		COMMIT TRANSACTION TRANSACCIONHISTORICOCAC;
END TRY  
BEGIN CATCH  
    SELECT   
        ERROR_NUMBER() AS ErrorNumber  
        ,ERROR_SEVERITY() AS ErrorSeverity  
        ,ERROR_STATE() AS ErrorState  
        ,ERROR_PROCEDURE() AS ErrorProcedure  
        ,ERROR_LINE() AS ErrorLine  
        ,ERROR_MESSAGE() AS ErrorMessage;  

    IF @@TRANCOUNT > 0  
        ROLLBACK TRANSACTION TRANSACCIONHISTORICOCAC;  
END CATCH;

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de cierre anual del programa de control de cáncer (CAC): genera el histórico de los formularios de seguimiento oncológico del año anterior copiando los datos vigentes de HCONCOPREG hacia la tabla de históricos HCONCOPREGHISTORICO, identificando el año de corte automáticamente. Luego elimina del registro activo a los pacientes marcados como fallecidos y reinicia todas las variables de seguimiento del formulario CAC a sus valores predeterminados para iniciar el nuevo ciclo anual. Está diseñado para ejecutarse el 1 de enero de cada año y garantiza integridad mediante una transacción completa con manejo de errores.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_GenerarHistoricoCAC';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_GenerarHistoricoCAC';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'"Archiva el cierre anual de los registros oncológicos del CAC en una tabla histórica, respalda la tabla operativa, depura pacientes fallecidos y resetea las variables de seguimiento para iniciar el nuevo año."', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_GenerarHistoricoCAC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la tabla HCONCOPREG con la estructura de columnas numéricas del CAC.; Debe existir la tabla destino HCONCOPREGHISTORICO con columnas compatibles incluyendo ANIO, FECHAREGISTRO, CONFIRMARESTADIO36/38 e IDHCORDQUIMIO53_1/66_1.; Se asume ejecución programada al cierre de año (ej. 01/01 11:59) para que el año calculado corresponda al período a archivar.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_GenerarHistoricoCAC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El año histórico generado siempre corresponde al año anterior al actual (year(GETDATE())-1).; Los pacientes marcados como fallecidos (campo [127]=''2'') no permanecen en la tabla operativa tras el cierre anual.; Tras el cierre, los campos de seguimiento CAC quedan reseteados a valores centinela (''98'', ''97'', ''2'', ''4'', ''0'' o fecha ''1845-01-01'' según el campo).; Toda la operación se ejecuta dentro de una transacción nombrada (TRANSACCIONHISTORICOCAC) con rollback en caso de error.; Se garantiza idempotencia para el año procesado eliminando previamente los registros del histórico con ese ANIO antes de insertar.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_GenerarHistoricoCAC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'CAC (Cuenta de Alto Costo); Oncología; Pacientes fallecidos; Histórico anual de registros oncológicos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_GenerarHistoricoCAC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] dbo.HCONCOPREGHISTORICO_backup_HCONCOPREG_DATOS_2024: Se crea/recrea la tabla HCONCOPREG_DATOS_2024 como copia completa (SELECT INTO) de HCONCOPREG, previo DROP IF EXISTS, como respaldo del corte anual.; [DELETE] dbo.HCONCOPREGHISTORICO: Antes de insertar, elimina todos los registros del histórico cuyo ANIO sea igual al año calculado (year(GETDATE())-1) para evitar duplicados.; [INSERT] dbo.HCONCOPREGHISTORICO: Inserta todos los registros actuales de HCONCOPREG en el histórico, fijando ANIO = año anterior, FECHAREGISTRO = GETDATE() y CONFIRMARESTADIO36, CONFIRMARESTADIO38, IDHCORDQUIMIO53_1, IDHCORDQUIMIO66_1 en NULL.; [DELETE] dbo.HCONCOPREG: Tras archivar, elimina de HCONCOPREG los pacientes con campo [127]=''2'' (marcados como fallecidos).; [UPDATE] dbo.HCONCOPREG: Resetea masivamente las variables CAC en HCONCOPREG: campos numéricos a ''98''/''97''/''2''/''4''/''0'', campos de fecha a ''1845-01-01'' y los adicionales AdicionalCAC13/14/16 a ''0'' y AdicionalCAC25 a ''98'', dejando los registros listos para el nuevo período.; [RETURN_RESULT] (error): Si ocurre una excepción, retorna un result-set con ERROR_NUMBER, ERROR_SEVERITY, ERROR_STATE, ERROR_PROCEDURE, ERROR_LINE y ERROR_MESSAGE, y hace ROLLBACK de la transacción si @@TRANCOUNT>0.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_GenerarHistoricoCAC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'common.Getdate', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_GenerarHistoricoCAC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCONCOPREG', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_GenerarHistoricoCAC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_GenerarHistoricoCAC';
-- GO
