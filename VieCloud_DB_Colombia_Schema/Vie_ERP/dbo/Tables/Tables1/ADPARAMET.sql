CREATE TABLE [dbo].[ADPARAMET] (
    [CODCENATE]                 CHAR (10)    NOT NULL,
    [TRINUMCLA]                 INT          NULL,
    [TRINUMTR1]                 INT          NULL,
    [TRINUMTR2]                 INT          NULL,
    [TRINUMTR3]                 INT          NULL,
    [TRINUMTR4]                 INT          NULL,
    [VIGMESING]                 INT          NULL,
    [INGAUTOMA]                 BIT          NULL,
    [INGHOSPIT]                 BIT          NULL,
    [PERMODTRI]                 BIT          NULL,
    [SALARIMIN]                 MONEY        NULL,
    [INDAUDFOR]                 NUMERIC (18) NOT NULL,
    [SERVACINTE]                CHAR (1)     NULL,
    [NUMHORATE]                 INT          NULL,
    [TRINUMTR5]                 INT          NULL,
    [SERRASANTI]                CHAR (20)    NULL,
    [NUMHORHOS]                 INT          NULL,
    [IDDESCRIPCIONRELACIONADA]  INT          NULL,
    [CREARPACIENTEDATOSBASICOS] BIT          NULL,
    [AllowPreTriage]            BIT          NULL,
    CONSTRAINT [PK_ADPARAMET] PRIMARY KEY CLUSTERED ([CODCENATE] ASC),
    CONSTRAINT [FK_ADPARAMET_INCUPSIPS] FOREIGN KEY ([SERRASANTI]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT) para habilitar creación de pre-triage; permite triaje previo antes de admisión formal en centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'AllowPreTriage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permitir creación del Pre Triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'AllowPreTriage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'AllowPreTriage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT) para crear automáticamente paciente en triage con datos básicos (identificación, nombre); 1=SÍ, 0=NO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'CREARPACIENTEDATOSBASICOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permitir crear paciente triage con datos básicos:   1 - SI   2 - NO  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'CREARPACIENTEDATOSBASICOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'CREARPACIENTEDATOSBASICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador entero (INT) de descripción de relación con ERP/Contrato; referencia a VIE ERP tabla CUPSEntityContractDescriptions para CUPS parametrizados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetro entero (INT) de horas para re-ingreso en hospitalización; controla ventana de tiempo para reabrir ingresos hospitalizados en Admisiones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'NUMHORHOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parámetro para establecer el numero de horas para un reigreso en Hospitalización, el parámetro esta en Parámetros de Admisiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'NUMHORHOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'NUMHORHOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de servicio IPS (CHAR 20, FK→INCUPSIPS.CODSERIPS) parametrizado para rastreo de anticuerpos en módulo de hemocomponentes; servicio especializado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'SERRASANTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicio IPS parametrizado para Rastreo de anticuerpos en modulo de hemocomponentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'SERRASANTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'SERRASANTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración máxima en minutos (INT) para atención inicial en TRIAGE nivel 5 (prioridad baja); parámetro de SLA de atención en urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'TRINUMTR5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de minutos maximo para la atencion inicial en TRIAGE 5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'TRINUMTR5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'TRINUMTR5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetro entero (INT) de horas para re-ingreso en urgencias; controla ventana de tiempo para reabrir ingresos de atención urgente en Admisiones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'NUMHORATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parámetro para establecer el numero de horas para un reigreso en Urgencia, el parámetro debe estar en Parámetros de Admisiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'NUMHORATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'NUMHORATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de carga de citas desde consulta externa (CHAR 1): 0=Ninguna, 1=Externo, 2=Nativo; origen de datos para programación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'SERVACINTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Carga citas desde Consulta Externa (0- Ninguna 2- Nativo 1 - Externo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'SERVACINTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'SERVACINTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de control de auditoría (NUMERIC 18); PK compuesta para trazabilidad y conformidad normativa en centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salario mínimo legal (MONEY) vigente en centro de atención; usado para cálculos de contratación y honorarios de profesionales de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'SALARIMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Salario Minimo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'SALARIMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'SALARIMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT) para omitir diagnóstico sindromático y permitir modificación de triage; flexibiliza clasificación en urgencia/emergencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'PERMODTRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Omitir Dx Sindromatico y Permitir Modificar Triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'PERMODTRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'PERMODTRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT) para permitir ingresos hospitalarios en centro de atención; True=habilitado para hospitalizaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'INGHOSPIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permitir Ingresos Hospitalarios:  True = Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'INGHOSPIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'INGHOSPIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT) para crear ingreso automático durante triage; acelera flujo de admisión en urgencias/emergencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'INGAUTOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Crear Ingreso Automatico en Triage:  True = Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'INGAUTOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'INGAUTOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vigencia de ingresos en meses (INT); parámetro que impide abrir ingresos con fecha anterior (fecha actual - días establecidos) en Admisiones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'VIGMESING';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vigencia de Ingresos en meses. Esta opcion no permite abrir ingresos de una fecha inferior a la fecha actual menos los dias establecidos en el control', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'VIGMESING';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'VIGMESING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración máxima en minutos (INT) para atención inicial en TRIAGE nivel 4 (prioridad media); parámetro de SLA de atención en urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'TRINUMTR4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de minutos maximo para la atencion inicial en TRIAGE 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'TRINUMTR4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'TRINUMTR4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración máxima en minutos (INT) para atención inicial en TRIAGE nivel 3 (prioridad media-alta); parámetro de SLA de atención en urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'TRINUMTR3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de minutos maximo para la atencion inicial en TRIAGE 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'TRINUMTR3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'TRINUMTR3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración máxima en minutos (INT) para atención inicial en TRIAGE nivel 2 (prioridad alta); parámetro de SLA de atención en urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'TRINUMTR2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de minutos maximo para la atencion inicial en TRIAGE 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'TRINUMTR2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'TRINUMTR2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración máxima en minutos (INT) para atención inicial en TRIAGE nivel 1 (prioridad máxima/emergencia); parámetro de SLA crítico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'TRINUMTR1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de minutos maximo para la atencion inicial en TRIAGE 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'TRINUMTR1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'TRINUMTR1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración máxima en minutos (INT) para clasificación/valoración inicial de triage; parámetro de SLA para proceso de triaje en urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'TRINUMCLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de minutos Maximo para la Clasificacion de TRIAGE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'TRINUMCLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'TRINUMCLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de centro de atención (CHAR 10, PK) donde ingresa el paciente; identifica unidad funcional/sede de atención en el ERP.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Centron de Atencion en donde Ingresa el Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de configuración por centro de atención para el módulo de admisiones. Controla reglas de ingreso automático, hospitalización, triaje, vigencia de ingresos y umbrales operativos del centro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET';
