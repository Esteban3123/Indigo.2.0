CREATE TABLE [dbo].[HCNUTRICIONC] (
    [ID]                   INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]            VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]            CHAR (10)                                                                        NOT NULL,
    [FECHREGIS]            DATE                                                                             NOT NULL,
    [CODCENATE]            CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]            CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]            CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [PESOPACIENTE]         INT                                                                              NOT NULL,
    [REQUECALORICO]        INT                                                                              NOT NULL,
    [APORTELIQUIDOS]       INT                                                                              NOT NULL,
    [TIEMPOADMINISTRACION] INT                                                                              NOT NULL,
    [VOLUMENNPT]           INT                                                                              NOT NULL,
    [VELOCIDADINFUSION]    INT                                                                              NOT NULL,
    [ORDEREGIST]           INT                                                                              NOT NULL,
    [RESULAGUA]            VARCHAR (4)                                                                      NULL,
    [RESULOSMOLARIDAD]     VARCHAR (4)                                                                      NULL,
    [RESULNITROGENO]       VARCHAR (4)                                                                      NULL,
    [RESULPROTEICAS]       VARCHAR (4)                                                                      NULL,
    [RESULCALCIO]          VARCHAR (4)                                                                      NULL,
    [RESULVOLUMEN]         VARCHAR (4)                                                                      NULL,
    CONSTRAINT [PK_HCNUTRICIONC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCNUTRICIONC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCNUTRICIONC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCNUTRICIONC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCNUTRICIONC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCNUTRICIONC] NOCHECK CONSTRAINT [FK_HCNUTRICIONC_ADCENATEN];


GO
ALTER TABLE [dbo].[HCNUTRICIONC] NOCHECK CONSTRAINT [FK_HCNUTRICIONC_ADINGRESO];


GO
ALTER TABLE [dbo].[HCNUTRICIONC] NOCHECK CONSTRAINT [FK_HCNUTRICIONC_INPACIENT];


GO
ALTER TABLE [dbo].[HCNUTRICIONC] NOCHECK CONSTRAINT [FK_HCNUTRICIONC_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el resultado Volumen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'RESULVOLUMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el resulatdo Calcio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'RESULCALCIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el resultado  Proteicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'RESULPROTEICAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el resultado Nitrogeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'RESULNITROGENO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el resultado Osmolaridad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'RESULOSMOLARIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el resultado Agua', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'RESULAGUA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Velocidad de Infusion (EN ml/H)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'VELOCIDADINFUSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Volumen de la NPT (EN ml)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'VOLUMENNPT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo de administración (Horas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'TIEMPOADMINISTRACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aporte de liquidos en la NPT (ml/Kg)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'APORTELIQUIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Requerimiento Calorico (KCAL/Kg)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'REQUECALORICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'PESOPACIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo profesional ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'FECHREGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'ID';

