CREATE TABLE [dbo].[CHREGCOII] (
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    CONSTRAINT [PK_CHREGCOII] PRIMARY KEY CLUSTERED ([IPCODPACI] ASC),
    CONSTRAINT [FK_CHREGCOII_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[CHREGCOII].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (cédula, identificación o documento único). Identificador único enmascarado tipo VARCHAR(25), clave primaria que referencia la tabla INPACIENT. Búsquedas: paciente, documento de identidad, identificación PII, número de cédula, atención médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGCOII', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGCOII', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGCOII', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de cobranza o cobro de cuentas de pacientes (CHREGCOII). Vincula al paciente con procesos de cobro, facturación o glosas, identificado por su cédula.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGCOII';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGCOII';
