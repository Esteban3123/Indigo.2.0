CREATE TABLE [dbo].[ADPOBESPEPAC] (
    [ID]          INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [IDADPOBESPE] INT                                                                              NULL,
    [ESTADO]      BIT                                                                              CONSTRAINT [DF_ADPOBESPEPAC_ESTADO] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_ADPOBESPEPAC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADPOBESPEPAC_ADPOBESPE] FOREIGN KEY ([IDADPOBESPE]) REFERENCES [dbo].[ADPOBESPE] ([ID]),
    CONSTRAINT [FK_ADPOBESPEPAC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[ADPOBESPEPAC] NOCHECK CONSTRAINT [FK_ADPOBESPEPAC_ADPOBESPE];


GO
ALTER TABLE [dbo].[ADPOBESPEPAC] NOCHECK CONSTRAINT [FK_ADPOBESPEPAC_INPACIENT];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADPOBESPEPAC].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
ALTER TABLE [dbo].[ADPOBESPEPAC] NOCHECK CONSTRAINT [FK_ADPOBESPEPAC_ADPOBESPE];


GO
ALTER TABLE [dbo].[ADPOBESPEPAC] NOCHECK CONSTRAINT [FK_ADPOBESPEPAC_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [IX_ADPOBESPEPAC]
    ON [dbo].[ADPOBESPEPAC]([IPCODPACI] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ADPOBESPEPAC_1]
    ON [dbo].[ADPOBESPEPAC]([IDADPOBESPE] ASC, [IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de estado activo (bit) que identifica si el paciente aún pertenece a la población especial registrada. Valores: 1=Activo, 0=Inactivo. Permite filtrar registros vigentes de pacientes en poblaciones vulnerables o prioritarias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPEPAC', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado    Me identifica si el paciente aun contiene la poblaicón especial  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPEPAC', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPEPAC', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT, FK) de la población especial a la que pertenece el paciente. Referencia a tabla ADPOBESPE. Agrupa pacientes por categoría de vulnerabilidad, condición especial o programa de atención diferenciada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPEPAC', @level2type = N'COLUMN', @level2name = N'IDADPOBESPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Población especial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPEPAC', @level2type = N'COLUMN', @level2name = N'IDADPOBESPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPEPAC', @level2type = N'COLUMN', @level2name = N'IDADPOBESPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente (VARCHAR 25, enmascarado PII como Identification_Ofuscado). Equivalente a cédula, documento de identidad o número de identificación. Identificador principal para búsqueda de paciente en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPEPAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPEPAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPEPAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (INT IDENTITY) y clave primaria de la tabla. Genera secuencia única para cada relación paciente-población especial. Uso interno para integridad referencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPEPAC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID autonumérico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPEPAC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPEPAC', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de observaciones especiales asociadas a pacientes en el contexto de admisiones. Vincula cada paciente con una observación especial específica e indica si dicha asociación está activa o inactiva.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPEPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPEPAC';
