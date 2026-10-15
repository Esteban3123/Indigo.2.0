CREATE TABLE [dbo].[HCFICHA455] (
    [ID]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT          NOT NULL,
    [CODDIAGNO]           CHAR (4)     NULL,
    [VISTORATAS]          BIT          NULL,
    [SISTALCANT]          BIT          NULL,
    [AGUASESTAN]          BIT          NULL,
    [DISPORESISOL]        BIT          NULL,
    [CUALOTRO]            VARCHAR (50) NULL,
    [FIEBRE]              BIT          NULL,
    [CEFALEA]             BIT          NULL,
    [MIALGIAS]            BIT          NULL,
    [HEPATOMEGALIA]       BIT          NULL,
    [ICTERICIA]           BIT          NULL,
    [PERROS]              BIT          NULL,
    [GATOS]               BIT          NULL,
    [BOVINOS]             BIT          NULL,
    [EQUINOS]             BIT          NULL,
    [PORCINOS]            BIT          NULL,
    [NINGUNO]             BIT          NULL,
    [OTROS]               BIT          NULL,
    [ACUEDUCTO]           BIT          NULL,
    [POZOCOMUN]           BIT          NULL,
    [RIO]                 BIT          NULL,
    [TANQUE]              BIT          NULL,
    [REPRESA]             BIT          NULL,
    [RIO2]                BIT          NULL,
    [ARROYO]              BIT          NULL,
    [LAGO]                BIT          NULL,
    [SINATENCE]           BIT          NULL,
    [VERSION]             VARCHAR (20) NULL,
    CONSTRAINT [PK_HCFICHA455] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCFICHA455_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA455] NOCHECK CONSTRAINT [FK_HCFICHA455_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si  ahi Actividades recreactivas en represa , lago en los ultimos 30  dias  es seleccionada Sin antecedente   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'SINATENCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si  ahi Actividades recreactivas en represa , lago en los ultimos 30  dias  es seleccionada   Lago/Laguna  true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'LAGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si  ahi Actividades recreactivas en represa , lago en los ultimos 30  dias  es seleccionada  Arroyo  true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'ARROYO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si  ahi Actividades recreactivas en represa , lago en los ultimos 30  dias  es seleccionada   Rio  true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'RIO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si  ahi Actividades recreactivas en represa , lago en los ultimos 30  dias  es seleccionada   Represa   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'REPRESA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si  ahi Abastecimiento de agua  es seleccionada   Tanque   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'TANQUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si  ahi Abastecimiento de agua  es seleccionada   Rio   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'RIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si  ahi Abastecimiento de agua  es seleccionada   Pozo comunitario    true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'POZOCOMUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si  ahi Abastecimiento de agua  es seleccionada   Acueducto    true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'ACUEDUCTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si  Hay animales en casa   es seleccionada   Otros    true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'OTROS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si  Hay animales en casa   es seleccionada  Ninguno   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'NINGUNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si  Hay animales en casa   es seleccionada   Porcinos   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'PORCINOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si  Hay animales en casa   es seleccionada   Equinos   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'EQUINOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si  Hay animales en casa   es seleccionada   Bovinos    true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'BOVINOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si  Hay animales en casa   es seleccionada   Gatos    true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'GATOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si  Hay animales en casa   es seleccionada   Perros     true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'PERROS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si signos y sintomas   es seleccionada   Ictericia     true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'ICTERICIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si signos y sintomas   es seleccionada   Hepatomegalia    true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'HEPATOMEGALIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si signos y sintomas   es seleccionada   Mialgias   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'MIALGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si signos y sintomas   es seleccionada   Cefalea      true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'CEFALEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si signos y sintomas   es seleccionada Fiebre     true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda   Cual  Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'CUALOTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda disposicuión de residuos  solidos  1 =  Recolección  0 =disposición pericomiciliaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'DISPORESISOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el contacto con aguas estancadas en los ultimos 30 dias   1 = Si    0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'AGUASESTAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  cuenta con sistema de alcantarillado   1 = Si  0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'SISTALCANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda visto ratas dentro o alrededor de su domicilio o lugar de trabajo   1 = Si   0  = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'VISTORATAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda  codigo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'ID';

