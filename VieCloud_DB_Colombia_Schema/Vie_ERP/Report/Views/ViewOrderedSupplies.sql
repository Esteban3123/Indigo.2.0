
CREATE VIEW [Report].[ViewOrderedSupplies]
AS
     SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
            CEN.NOMCENATE [CENTRO ATENCION], 
            UNI.UFUDESCRI [UNIDAD FUNCIONAL], 
            HEA.Code 'CODIGO ENTIDAD', 
            HEA.Name 'ENTIDAD', 
            CGR.Name 'GRUPO DE ATENCION', 
            CAST(FECHAORDE AS DATE) [FECHA SOLICITUD], 
            SAL.NOMMEDICO [PROFESIONAL], 
            CAB.IPCODPACI [IDENTIFICACION], 
            PAC.IPNOMCOMP [PACINETE], 
            CAB.NUMINGRES [INGRESO], 
            DET.CODPRODUC [CODIGO PRODUCTO], 
            PRO.DESPRODUC [DESCRIPCION PRODUCTO],
            CASE PRO.TIPPRODUC
                WHEN 3
                THEN 'MEDICAMENTO COMO INSUMO'
                WHEN 2
                THEN 'INSUMO'
            END [TIPO PRODUCTO], 
            CANPEDPRO [CANTIDAD PRODUCTO],
			CAST(FECHAORDE AS DATE) [FECHA BUSQUEDA], 
			YEAR(FECHAORDE) AS 'AÑO FECHA BUSQUEDA', 
			MONTH(FECHAORDE) AS 'MES AÑO FECHA BUSQUEDA', 
			CASE MONTH(FECHAORDE) 
			     WHEN 1 THEN 'ENERO'	  
				 WHEN 2 THEN 'FEBRERO'
				 WHEN 3 THEN 'MARZO'
				 WHEN 4 THEN 'ABRIL'
				 WHEN 5 THEN 'MAYO'
				 WHEN 6 THEN 'JUNIO'
				 WHEN 7 THEN 'JULIO'
				 WHEN 8 THEN 'AGOSTO'
				 WHEN 9 THEN 'SEPTIEMBRE'
				 WHEN 10 THEN 'OCTUBRE'
				 WHEN 11 THEN 'NOVIEMBRE'
				 WHEN 12 THEN 'DICIEMBRE' END AS 'MES NOMBRE FECHA BUSQUEDA', 
		    DAY(FECHAORDE) AS 'DIA FECHA BUSQUEDA',
			CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
     FROM dbo.HCSOLINSC AS CAB WITH(NOLOCK)
          INNER JOIN dbo.HCSOLINSD AS DET WITH(NOLOCK) ON CAB.CODCONCEC = DET.CODCONCEC
          INNER JOIN dbo.ADINGRESO AS ING WITH(NOLOCK) ON ING.IPCODPACI = CAB.IPCODPACI AND ING.NUMINGRES = CAB.NUMINGRES
          INNER JOIN dbo.INPACIENT AS PAC WITH(NOLOCK) ON CAB.IPCODPACI = PAC.IPCODPACI
          INNER JOIN dbo.IHLISTPRO AS PRO WITH(NOLOCK) ON DET.CODPRODUC = PRO.CODPRODUC
          INNER JOIN dbo.ADCENATEN AS CEN WITH(NOLOCK) ON CAB.CODCENATE = CEN.CODCENATE
          INNER JOIN dbo.INUNIFUNC AS UNI WITH(NOLOCK) ON CAB.UFUCODIGO = UNI.UFUCODIGO
          INNER JOIN dbo.INPROFSAL AS SAL WITH(NOLOCK) ON DET.CODPROSAL = SAL.CODPROSAL
          INNER JOIN [Contract].HealthAdministrator HEA WITH(NOLOCK) ON ING.GENCONENTITY = HEA.ID
          INNER JOIN [Contract].CareGroup AS CGR WITH(NOLOCK) ON ING.GENCAREGROUP = CGR.Id
     WHERE PRO.TIPPRODUC <> '1';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte orientada a consumo analítico y BI que consolida las solicitudes de insumos y medicamentos-como-insumo (excluye productos tipo 1) generadas durante ingresos hospitalarios. Cruza la cabecera y el detalle de la solicitud con datos del paciente, profesional solicitante, producto del catálogo, centro de atención, unidad funcional, entidad pagadora y grupo de atención. Expone la fecha de solicitud descompuesta en año, mes (numérico y en español) y día para facilitar filtros temporales en herramientas de reporting.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrderedSupplies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrderedSupplies';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida las solicitudes de insumos y medicamentos-como-insumo realizadas durante ingresos, enriquecidas con paciente, profesional, centro/unidad de atención, entidad administradora y grupo de cuidado.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrderedSupplies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada solicitud (HCSOLINSC) debe tener detalle (HCSOLINSD) por CODCONCEC.; El ingreso referenciado (IPCODPACI + NUMINGRES) debe existir en ADINGRESO.; El paciente, producto, centro de atención, unidad funcional y profesional deben existir en sus respectivos maestros.; El ingreso debe tener asociadas una entidad administradora (GENCONENTITY) y un grupo de atención (GENCAREGROUP) válidos en Contract.HealthAdministrator y Contract.CareGroup.; Los productos deben tener TIPPRODUC distinto de ''1'' (excluye productos que no son insumo ni medicamento-como-insumo).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrderedSupplies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan solicitudes cuyo producto NO sea de tipo ''1''; siempre se incluyen únicamente insumos (TIPPRODUC=2) y medicamentos-como-insumo (TIPPRODUC=3).; Todos los joins son INNER JOIN: si falta cualquier maestro relacionado (paciente, producto, centro, unidad, profesional, administradora, grupo de atención), la fila no aparece.; ID_COMPANY siempre se entrega como el nombre de la base de datos actual truncado a 9 caracteres.; ULT_ACTUAL siempre se calcula con la hora actual convertida a la zona ''Pakistan Standard Time''.; FECHA SOLICITUD y FECHA BUSQUEDA se exponen siempre como DATE (sin hora) derivadas de FECHAORDE.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrderedSupplies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'solicitud de insumos; medicamento como insumo; insumo médico; ingreso hospitalario; paciente; profesional de la salud; centro de atención; unidad funcional; entidad administradora de salud; grupo de atención; producto farmacéutico', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrderedSupplies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewOrderedSupplies: Devuelve una fila por detalle de solicitud de insumo/medicamento-como-insumo, filtrando IHLISTPRO.TIPPRODUC <> ''1'' y clasificando TIPPRODUC=3 como ''MEDICAMENTO COMO INSUMO'' y TIPPRODUC=2 como ''INSUMO''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrderedSupplies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PRO.TIPPRODUC = 3 → Etiqueta TIPO PRODUCTO = ''MEDICAMENTO COMO INSUMO''; si PRO.TIPPRODUC = 2 → Etiqueta TIPO PRODUCTO = ''INSUMO''; si PRO.TIPPRODUC = ''1'' → Se excluye del resultado (WHERE PRO.TIPPRODUC <> ''1'') else Se incluye en el reporte; si MONTH(FECHAORDE) entre 1 y 12 → Mapea el número de mes al nombre del mes en español (ENERO..DICIEMBRE)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrderedSupplies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCSOLINSC; dbo.HCSOLINSD; dbo.ADINGRESO; dbo.INPACIENT; dbo.IHLISTPRO; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INPROFSAL; Contract.HealthAdministrator; Contract.CareGroup', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrderedSupplies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrderedSupplies';
GO
