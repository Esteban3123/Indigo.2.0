CREATE TABLE [dbo].[HCPerfilFarmaD] (
    [ID]               INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPERFILFARMAC] INT                                                                              NOT NULL,
    [NUMINGRES]        CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]        VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODPRODUC]        CHAR (20)                                                                        NOT NULL,
    [DOSISPROD]        NUMERIC (18, 2)                                                                  NULL,
    [CODUNIMED]        VARCHAR (20)                                                                     NULL,
    [FRECUENCI]        INT                                                                              NULL,
    [UNIFRECUE]        CHAR (1)                                                                         NULL,
    [FECINIDOS]        DATETIME                                                                         NULL,
    [TIPFORMED]        CHAR (1)                                                                         NULL,
    [DURACIDOS]        CHAR (20)                                                                        NULL,
    [CANPEDPRO]        INT                                                                              NOT NULL,
    CONSTRAINT [PK_HCPerfilFarmaD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPerfilFarmaD_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPerfilFarmaD_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPerfilFarmaD].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de productos/medicamentos asignados al perfil farmacológico de un paciente durante su ingreso hospitalario. Registra dosis, frecuencia, duración y cantidad pedida de cada medicamento en el esquema farmacológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de detalle del perfil farmacológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al encabezado del perfil farmacológico al que pertenece este medicamento (enlace con la cabecera del perfil).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'IDHCPERFILFARMAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'IDHCPERFILFARMAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso u hospitalización del paciente al que corresponde el perfil farmacológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cédula del paciente, identificación o documento de identidad (valor ofuscado por seguridad).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del medicamento o producto farmacéutico prescrito (código del ítem en el inventario de farmacia).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis del medicamento prescrita por cada administración (cantidad numérica de la dosis).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida de la dosis del medicamento (por ejemplo: mg, ml, unidades).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia de administración del medicamento expresada en valor numérico (cada cuántas unidades de tiempo se administra).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'FRECUENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'FRECUENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo de la frecuencia de administración (por ejemplo: H=horas, D=días).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio de la dosificación o del esquema del medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'FECINIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'FECINIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de forma farmacéutica o vía de administración del medicamento (por ejemplo: oral, intravenosa, tópica).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'TIPFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'TIPFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración total del esquema de dosificación del medicamento (período durante el cual se debe administrar).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'DURACIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'DURACIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pedida o solicitada del producto farmacéutico a farmacia para cubrir el esquema de dosificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPerfilFarmaD', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
