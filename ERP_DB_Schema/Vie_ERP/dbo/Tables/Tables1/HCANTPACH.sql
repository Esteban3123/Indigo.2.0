CREATE TABLE [dbo].[HCANTPACH] (
    [IPCODPACI]         VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')      NOT NULL,
    [ANTMEDPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTQUIPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTTRAPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTINMPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTALEPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTTRUPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTPSIPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTFARPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTFAMPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTTOXPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTOTRPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [INDAUDFOR]         NUMERIC (18)                                                                          NOT NULL,
    [ANTANESTE]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTHABVID]         VARCHAR (MAX)                                                                         NULL,
    [ANTESCOLARES]      VARCHAR (MAX)                                                                         NULL,
    [ANTLABORALES]      VARCHAR (MAX)                                                                         NULL,
    [ANTNUTRICION]      VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTODONTOLOG]      VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTSOCIOECON]      VARCHAR (MAX)                                                                         NULL,
    [ANTUROLOGIASEXUAL] VARCHAR (MAX)                                                                         NULL,
    [Ophthalmological]  VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    CONSTRAINT [PK_HCANTPACH] PRIMARY KEY CLUSTERED ([IPCODPACI] ASC),
    CONSTRAINT [FK_HCANTPACH_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCANTPACH] NOCHECK CONSTRAINT [FK_HCANTPACH_INPacient];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes Oftalmológicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'Ophthalmological';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Urologia Sexual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTUROLOGIASEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes SocioEconómicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTSOCIOECON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes Odontológicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTODONTOLOG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes Nutricionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTNUTRICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes  Laborales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTLABORALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes Escolares', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTESCOLARES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes Habitos de Vida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTHABVID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes Anestésicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTANESTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTOTRPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes Toxicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTTOXPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes Familiares', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTFAMPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes Farmacologicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTFARPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes Psicologicos y Psiquiatricos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTPSIPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes Traumaticos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTTRUPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes Alergicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTALEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes Inmunologicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTINMPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes Transfucionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTTRAPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes Quirurgicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTQUIPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes Medicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTMEDPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'IPCODPACI';

