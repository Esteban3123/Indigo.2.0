CREATE TABLE [dbo].[INPACIENTTOPANU] (
    [Id]        INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [ANIO]      INT                                                                              NOT NULL,
    [PAGADOCMO] NUMERIC (18)                                                                     NOT NULL,
    [PAGADOCOP] NUMERIC (18)                                                                     NOT NULL,
    [PAGADOCRE] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_INPACIENTTOPANU] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_INPACIENTTOPANU_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPACIENTTOPANU].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
CREATE NONCLUSTERED INDEX [IX_INPACIENTTOPANU_IPCODPACI_ANIO]
    ON [dbo].[INPACIENTTOPANU]([IPCODPACI] ASC, [ANIO] ASC)
    INCLUDE([PAGADOCMO], [PAGADOCOP], [PAGADOCRE]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor pagado por cuota de recuperación (copago recuperación). NUMERIC(18). Monto asumido por el paciente en régimen especial o contributivo para servicios de recuperación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU', @level2type = N'COLUMN', @level2name = N'PAGADOCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor pagado por cuota de recuperación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU', @level2type = N'COLUMN', @level2name = N'PAGADOCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU', @level2type = N'COLUMN', @level2name = N'PAGADOCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor pagado por régimen contributivo (copago contributivo). NUMERIC(18). Monto de pago del paciente afiliado a sistema contributivo por atención en salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU', @level2type = N'COLUMN', @level2name = N'PAGADOCOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor pagado por regimen contributivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU', @level2type = N'COLUMN', @level2name = N'PAGADOCOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU', @level2type = N'COLUMN', @level2name = N'PAGADOCOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor pagado por cuota moderadora (copago moderador). NUMERIC(18). Monto de participación del paciente en el costo de servicios según arancel.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU', @level2type = N'COLUMN', @level2name = N'PAGADOCMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor pagado por cuota moderadora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU', @level2type = N'COLUMN', @level2name = N'PAGADOCMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU', @level2type = N'COLUMN', @level2name = N'PAGADOCMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año de referencia para el tope de pagos (período fiscal/anual). INT. Año en que se registran y controlan los montos máximos pagados por paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU', @level2type = N'COLUMN', @level2name = N'ANIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el año en que se va a manejar el tope', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU', @level2type = N'COLUMN', @level2name = N'ANIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU', @level2type = N'COLUMN', @level2name = N'ANIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Paciente (identificación, cédula, documento). VARCHAR(25), Identification_Ofuscado (PII). Identificador único del paciente. FK a INPACIENT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de registro (clave primaria). INT IDENTITY(1,1). Secuencial interno de la tabla de topes de pagos por año.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro anual de montos pagados por paciente, discriminando los pagos realizados por modalidad: copago, moderadora y recobro. Permite controlar el acumulado de pagos del paciente por año para validar topes y obligaciones financieras.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU';
