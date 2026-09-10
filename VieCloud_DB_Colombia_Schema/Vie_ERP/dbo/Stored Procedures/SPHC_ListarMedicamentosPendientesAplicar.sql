CREATE PROCEDURE [dbo].[SPHC_ListarMedicamentosPendientesAplicar]
(
 @CentroAtencion  CHAR(10), 
 @UnidadFuncional CHAR(10)
)
AS
    BEGIN
        DECLARE @FechaInicio AS DATETIME;

        SET @FechaInicio = [Common].[GETDATE]();
        SET NOCOUNT ON;

		WITH DATOS AS (
			SELECT CONSECUTI AS Consecutivo, 
				   RTRIM(A.IPCODPACI) AS Identificacion, 
				   RTRIM(B.IPNOMCOMP) AS Paciente, 
				   RTRIM(DESPRODUC) AS Medicamento, 
				   FECPROAPL AS 'Fecha de Aplicacion',
				   CASE
					   WHEN RTRIM(A.DOSISPROD) IS NULL
					   THEN A.DESADMINI
					   ELSE RTRIM(A.DOSISPROD) + ' ' + RTRIM(ABRUNIMED)
				   END AS Dosis, 
				   A.CODUNIMED AS UnidadDosisAplicada,
				   CASE
					   WHEN DATEDIFF(MINUTE, FECPROAPL, [Common].[GETDATE]()) < 0
					   THEN 0
					   ELSE DATEDIFF(MINUTE, FECPROAPL, [Common].[GETDATE]())
				   END AS Minutos,
				   CASE
					   WHEN DATEDIFF(MINUTE, FECPROAPL, [Common].[GETDATE]()) < 0
					   THEN 0
					   ELSE DATEDIFF(MINUTE, FECPROAPL, [Common].[GETDATE]())
				   END AS Barra, 
				   A.CODPRODUC AS 'Codigo Medicamento', 
				   A.NUMINGRES AS Ingreso, 
				   PREESTADO AS EstadoPrescripcion,
				   CASE PREESTADO
					   WHEN '1'
					   THEN 'Medicamentos Programados'
					   WHEN '7'
					   THEN 'Medicamentos para Pacientes con Salida'
					   ELSE 'Medicamentos Sin Ordenar'
				   END AS Estado, 
				   A.DURACIDOS AS TTipoDuracion, 
				   L.CODTIPPAC AS TipoPaciente, 
				   RTRIM(G.CODICAMAS) + '  ' + RTRIM(K.DESCCAMAS) AS Cama, 
				   C.CODDCIMED AS 'DCI',A.IDHCORDQUIMIO,IDHCORDCICLOSD,IDCITA
				   , U.TIPUNIDAD AS 'TipoUnidadFuncional'
				   ,CASE WHEN A.IDHCORDQUIMIO IS NULL
				   THEN dbo.ValidateMedicationStatus(A.CONSECPRESCRA, A.CODPRODUC)
				   ELSE CASE dbo.ValidateSchemaStatus(2,A.IDHCORDQUIMIO, A.CODPRODUC, CICLO.CICLO, CICLO.DIA) WHEN 2 THEN 1 WHEN 3 THEN 1 WHEN 4 THEN 2 ELSE 0 END
				   END AS MODIFICADO
			FROM dbo.HCHOJAMED A
					 INNER JOIN dbo.INPACIENT B With (NoLock) ON A.IPCODPACI = B.IPCODPACI
					 INNER JOIN dbo.ADINGRESO L With (NoLock) ON L.NUMINGRES = A.NUMINGRES
					 INNER JOIN dbo.CHREGESTA G With (NoLock) ON A.IPCODPACI = G.IPCODPACI  AND A.NUMINGRES = G.NUMINGRES AND G.REGESTADO = 1
					 left outer JOIN dbo.CHCAMASHO K With (NoLock) ON K.CODICAMAS = G.CODICAMAS		 
					 INNER JOIN dbo.IHLISTPRO C With (NoLock) ON A.CODPRODUC = C.CODPRODUC
					 LEFT OUTER JOIN dbo.HCPRESCRA  D With (NoLock) ON A.IPCODPACI = D.IPCODPACI
														AND A.NUMINGRES = D.NUMINGRES
														AND A.CODPRODUC = D.CODPRODUC
														AND ISNULL(D.IDHCORDQUIMIO,0) = ISNULL(A.IDHCORDQUIMIO,0)
														AND PREESTADO IN('1', '7')
					 INNER JOIN dbo.INUNIMEDI U With (NoLock) ON A.CODUNIMED = U.CODUNIMED		
					 LEFT JOIN [EHR].[HCORDCICLOSD] AS CICLO ON A.IDHCORDCICLOSD = CICLO.ID
					 --LEFT OUTER JOIN dbo.ADTRIAGEU H With (NoLock) ON H.NUMINGRES = A.NUMINGRES
					 --LEFT OUTER JOIN dbo.ADCONTURG I With (NoLock) ON I.CODCONCEC = H.CODCONCEC				 
			WHERE A.CODCENATE = @CentroAtencion
				  --AND A.UFUCODIGO = @UnidadFuncional (Se comenta para que liste la cama actual y no de donde fue programado)
				  AND K.UFUCODIGO = @UnidadFuncional --con esto va a la cama actual del paciente
				  AND A.MEDESTADO = '1'
				  AND A.CABESTADO = '1'
				  AND G.REGESTADO = 1
				  AND A.FECPROAPL < @FechaInicio
			  ) SELECT * FROM DATOS
			 
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los medicamentos pendientes de aplicar (administrar) a pacientes hospitalizados en un centro de atención y unidad funcional específicos. Cruza la hoja de medicamentos (HCHOJAMED) con el maestro de pacientes (INPACIENT), el registro de ingresos (ADINGRESO), la estancia y cama actual del paciente (CHREGESTA, CHCAMASHO) y el catálogo de productos farmacéuticos (IHLISTPRO) para obtener, por cada dosis programada cuya fecha de aplicación ya venció, el nombre del paciente, medicamento, dosis, cama, minutos de retraso y estado de la prescripción. Diferencia entre medicamentos programados normales (estado 1), medicamentos para pacientes con salida (estado 7) y esquemas de quimioterapia (HCPRESCRA, IDHCORDQUIMIO), excluyendo aquellos cuyo estado de modificación sea 2 (ya administrados o cancelados). Es utilizado por el módulo de enfermería para el control y seguimiento de la administración de medicamentos en tiempo real.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarMedicamentosPendientesAplicar';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarMedicamentosPendientesAplicar';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los medicamentos pendientes de aplicar a pacientes hospitalizados en un centro y unidad funcional, mostrando datos del paciente, dosis, cama y tiempo transcurrido desde la hora programada de aplicación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarMedicamentosPendientesAplicar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener un registro de estado/estancia activo (REGESTADO = 1) en CHREGESTA.; La hoja de medicación debe estar activa (MEDESTADO = ''1'' y CABESTADO = ''1'').; La fecha programada de aplicación (FECPROAPL) debe ser anterior a la fecha/hora actual del sistema.; La cama actual del paciente debe pertenecer a la unidad funcional consultada.; El medicamento debe existir en el catálogo de productos y tener una unidad de medida válida.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarMedicamentosPendientesAplicar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran medicamentos cuya hora programada de aplicación ya pasó respecto al reloj del sistema.; El filtro de unidad funcional se aplica sobre la cama actual del paciente, no sobre la unidad donde se programó la medicación.; Solo se incluyen prescripciones cuya hoja de medicación esté activa (MEDESTADO=''1'' y CABESTADO=''1'').; Los minutos transcurridos nunca son negativos (se normalizan a 0).; Los medicamentos de esquemas de quimioterapia cuyo estado de validación sea 4 nunca se listan.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarMedicamentosPendientesAplicar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamento pendiente de aplicar; Hoja de medicación; Prescripción médica; Dosis y unidad de medida; Hora programada de aplicación; Cama hospitalaria; Unidad funcional; Tipo de paciente; Esquema de quimioterapia (ciclo/día); Paciente con salida; Estado de prescripción', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarMedicamentosPendientesAplicar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] DATOS (CTE resultado): Devuelve solo registros con MODIFICADO <> 2; los esquemas de quimioterapia con estado de validación 4 (MODIFICADO=2) se excluyen del resultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarMedicamentosPendientesAplicar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DOSISPROD es NULL → Se muestra como Dosis la descripción de administración (DESADMINI) else Se concatena DOSISPROD con la abreviatura de unidad de medida (ABRUNIMED); si DATEDIFF(MINUTE, FECPROAPL, ahora) < 0 → Minutos y Barra se fijan en 0 (aplicación aún no vencida) else Se calculan minutos transcurridos desde la fecha programada; si PREESTADO = ''1'' → Se etiqueta como ''Medicamentos Programados'' else Si PREESTADO=''7'' → ''Medicamentos para Pacientes con Salida''; cualquier otro → ''Medicamentos Sin Ordenar''; si IDHCORDQUIMIO IS NULL (medicamento no es de esquema de quimioterapia) → Se calcula MODIFICADO con dbo.ValidateMedicationStatus sobre la prescripción else Se calcula MODIFICADO con dbo.ValidateSchemaStatus para el ciclo/día del esquema (2/3→1, 4→2, otro→0)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarMedicamentosPendientesAplicar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE; dbo.ValidateMedicationStatus; dbo.ValidateSchemaStatus', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarMedicamentosPendientesAplicar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHOJAMED; dbo.INPACIENT; dbo.ADINGRESO; dbo.CHREGESTA; dbo.CHCAMASHO; dbo.IHLISTPRO; dbo.HCPRESCRA; dbo.INUNIMEDI; EHR.HCORDCICLOSD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarMedicamentosPendientesAplicar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarMedicamentosPendientesAplicar';
-- GO
