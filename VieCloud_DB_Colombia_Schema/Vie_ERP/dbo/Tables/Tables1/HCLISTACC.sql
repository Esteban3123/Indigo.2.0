CREATE TABLE [dbo].[HCLISTACC] (
    [CODCONSEC]    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMENCENF]    VARCHAR (200) NOT NULL,
    [GENEROAPLI]   INT           NULL,
    [UNIEDADMI]    INT           NULL,
    [EDADMINIM]    INT           NULL,
    [UNIEDADMA]    INT           NULL,
    [EDADMAXIM]    INT           NULL,
    [ESTADOENC]    INT           NULL,
    [INAPLICA]     INT           NULL,
    [APMEDENF]     INT           NULL,
    [TIPO]         INT           CONSTRAINT [DF_HCLISTACC_TIPO] DEFAULT ((1)) NOT NULL,
    [APLICADIAGNO] BIT           NULL,
    CONSTRAINT [PK_HCLISTCHE] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que determina si la lista de chequeo aplica para todos los diagnósticos o para ninguno; usado en filtrado de protocolos clínicos y atención sanitaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'APLICADIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que indica si la lista de chequeo aplica "Para todos" o "Ninguno" diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'APLICADIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'APLICADIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de la lista de chequeo (INT): 1=Seguridad del paciente, 2=Cirugía, 3=Atención domiciliaria; define contexto clínico de aplicación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1: Seguridad paciente  2: Cirugía  3: Atención domiciliaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rol profesional responsable (INT): 1=Médico, 2=Enfermería; indica quién ejecuta la lista de chequeo en la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'APMEDENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1- Medico 2 - Enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'APMEDENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'APMEDENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Alcance de aplicabilidad (INT): 1=Aplica para todas las unidades funcionales/centros de atención, 0=No aplica a ninguna; usado en configuración institucional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'INAPLICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1- Aplica para todas las Unidades Funcionales  0- Ninguna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'INAPLICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'INAPLICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado operativo de la lista (INT): 1=Activa, 2=Inactiva; controla disponibilidad en procesos clínicos e ingreso de pacientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'ESTADOENC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado  1 activo -  2 inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'ESTADOENC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'ESTADOENC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad máxima permitida (INT) para aplicar la lista de chequeo; valor numérico que define rango etario superior.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'EDADMAXIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Maxima en la que aplica  la lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'EDADMAXIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'EDADMAXIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida para edad máxima (INT): 1=Años, 2=Meses, 3=Días; especifica escala temporal del límite superior.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'UNIEDADMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de Medida para Edad Maxima: 1: Años    2: Meses    3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'UNIEDADMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'UNIEDADMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad mínima permitida (INT) para aplicar la lista de chequeo; valor numérico que define rango etario inferior.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'EDADMINIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Minima en la que aplica la lista de Chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'EDADMINIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'EDADMINIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida para edad mínima (INT): 1=Años, 2=Meses, 3=Días; especifica escala temporal del límite inferior.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'UNIEDADMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de Medida para Edad Minima:  1: Años    2: Meses    3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'UNIEDADMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'UNIEDADMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Género/sexo para aplicabilidad (INT): 1=Masculino, 2=Femenino, 3=Aplica para ambos; filtra pacientes por sexo en protocolo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'GENEROAPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sexo Aplicacion Encuesta 1 Masculino  - 2 femenino - 3  Aplica para Ambas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'GENEROAPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'GENEROAPLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o nombre de la lista de chequeo (VARCHAR 200); texto que identifica protocolo, diagnóstico, procedimiento o atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'NOMENCENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la lista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'NOMENCENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'NOMENCENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY); clave primaria de la lista de chequeo en el sistema clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumarico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'CODCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de listas de chequeo o acciones clínicas de enfermería. Registra los formularios o plantillas de evaluación que se aplican a los pacientes según género, rango de edad y unidad de admisión, indicando si están activos y si aplican a diagnósticos o a médicos de enfermería.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC';
