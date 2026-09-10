CREATE TABLE [dbo].[HCFICHA110] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [RNTIPDOC]            INT           NULL,
    [RNIPCODPACI]         CHAR (15)     NULL,
    [RNFECHNACI]          DATE          NULL,
    [RNEDAD]              INT           NULL,
    [RNSEXO]              INT           NULL,
    [RNPESO]              INT           NULL,
    [RNTALLA]             INT           NULL,
    [GESTPARTO]           INT           NULL,
    [CLASIFPESO]          INT           NULL,
    [SITIOPARTO]          INT           NULL,
    [MULTIPLIEMBARA]      INT           NULL,
    [EMBARAZOPREVIOS]     INT           NULL,
    [NUMHIJOS]            INT           NULL,
    [NIVELEDUCATIVO]      INT           NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_FICHA110] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA110_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA110_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA110] NOCHECK CONSTRAINT [CK_HCFICHA110_JSON];




GO
ALTER TABLE [dbo].[HCFICHA110] NOCHECK CONSTRAINT [CK_HCFICHA110_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campos adicionales en formato JSON (VARCHAR MAX); validado con restricción ISJSON; almacena datos complementarios de la ficha de notificación de recién nacido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación (VARCHAR 20); NULL indica primera versión; controla evolución y cambios en el formulario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico CIE-10 del recién nacido (CHAR 4); identifica condición clínica o patología al nacimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel educativo de la madre (INT); 1=Primaria, 2=Secundaria, 3=Técnico/Superior, 4=Ninguna, 5=No sabe; DEPRECADO desde V01_2020-03-06', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'NIVELEDUCATIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel educativo de la madre :   1 = Primaria   2 = Secundaria   3 = Técnico o superior   4 = Ninguna   5 = No sabe o sin Información --> ( item eliminado desde version V01_2020-03-06 )   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'NIVELEDUCATIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'NIVELEDUCATIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de hijos vivos incluyendo el recién nacido actual (INT); historial de partos exitosos de la madre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'NUMHIJOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de los hijos vivos contando el actual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'NUMHIJOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'NUMHIJOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de embarazos previos a este parto (INT); antecedente obstétrico materno; multiparidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'EMBARAZOPREVIOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de embarazos previos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'EMBARAZOPREVIOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'EMBARAZOPREVIOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Multiplicidad del embarazo (INT); clasifica: 1=Simple, 2=Gemelar, 3=Múltiple; información obstétrica del nacimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'MULTIPLIEMBARA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Multiplicidad del embarazo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'MULTIPLIEMBARA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'MULTIPLIEMBARA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sitio de atención del parto (INT); ubica lugar de nacimiento: hospital, clínica, domicilio, vía o centro de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'SITIOPARTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sitio de atención del parto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'SITIOPARTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'SITIOPARTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del peso al nacimiento (INT); categoriza: bajo peso, normal, macrosómico; relevante para seguimiento neonatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'CLASIFPESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación peso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'CLASIFPESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'CLASIFPESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Semanas de gestación al momento del parto (INT); edad gestacional; 37-40 semanas = a término', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'GESTPARTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Semanas gestación al parto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'GESTPARTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'GESTPARTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Talla o longitud del recién nacido al nacer (INT, cm); medida antropométrica neonatal de crecimiento intrauterino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNTALLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Talla al nacer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNTALLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNTALLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso del recién nacido al nacer (INT, gramos); parámetro vital de viabilidad y desarrollo fetal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNPESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso al nacer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNPESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNPESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sexo del recién nacido (INT); 1=Masculino, 2=Femenino; dato demográfico obligatorio del RN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNSEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sexo RN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNSEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNSEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad del recién nacido en días (INT); tiempo transcurrido desde el nacimiento; seguimiento neonatal temprano', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNEDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad RN (días)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNEDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNEDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de nacimiento del recién nacido (DATE); día/mes/año del parto; base para cálculo de edad gestacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNFECHNACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recien nacido = RN  Fecha nacimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNFECHNACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNFECHNACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de identificación del paciente recién nacido (CHAR 15); cédula, documento, número de historia clínica PII', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNIPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recien nacido   Codigo del paciente ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNIPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNIPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento del recién nacido (INT); 1=RC (Registro Civil), 2=MS (Ministerio Salud), 3=PE (Pasaporte Extranjero), 4=CN (Cédula Nacional); NUEVO desde V01_2020-03-06', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNTIPDOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Documento del Recien Nacido -->    1 = RC      2 = MS    3 = PE    4 = CN  --> ( item nuevo desde version V01_2020-03-06 )  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNTIPDOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNTIPDOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de ficha de notificación (INT, FK); referencia a HCFICHANOTIFICACION; vinculación padre-hijo de registro epidemiológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de Ficha notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT, PK IDENTITY 1,1); clave primaria de la tabla HCFICHA110', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación clínica (sección 110) con datos perinatales y antropométricos del recién nacido o del evento notificado: tipo de documento, identificación del paciente, fecha de nacimiento, edad, sexo, peso, talla, gestación, clasificación del peso al nacer, lugar del parto, embarazos previos, número de hijos, nivel educativo de la madre y diagnóstico CIE-10 asociado. Sirve para el registro de eventos de vigilancia epidemiológica perinatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110';
