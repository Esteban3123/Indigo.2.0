CREATE TABLE [dbo].[HCPARAESCALAS] (
    [ID]                 INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]             VARCHAR (3)   NOT NULL,
    [NOMBREESCALA]       VARCHAR (MAX) NOT NULL,
    [SEXO]               INT           NOT NULL,
    [EDADMIN]            INT           NOT NULL,
    [EDADMAX]            INT           NOT NULL,
    [DASMEDICO]          BIT           NULL,
    [DASENFERMERIA]      BIT           NULL,
    [DASTERAPIAS]        BIT           NULL,
    [DASACADEMICO]       BIT           NULL,
    [DASESPECIALISTAS]   BIT           NULL,
    [DASSERVICIOAPOYO]   BIT           NULL,
    [TRIAGE]             BIT           NULL,
    [PharmaceuticalCare] BIT           NULL,
    CONSTRAINT [PK_HCPARAESCALAS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mostrar escala en el formulario de Triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'TRIAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Visible en Dashboard servicio de apoyo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'DASSERVICIOAPOYO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Visible en Dashboard especialistas ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'DASESPECIALISTAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Visible en Dashboard academico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'DASACADEMICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Visible en Terapias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'DASTERAPIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Visible en Dashboard enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'DASENFERMERIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Visible en Dashboard medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'DASMEDICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad maxima en meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'EDADMAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad minima en meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'EDADMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sexo  1 - masculino  2 - Femenino  3 - Ambos  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'SEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el nombre de la escala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'NOMBREESCALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la escala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'ID';

