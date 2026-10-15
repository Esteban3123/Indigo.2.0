CREATE TABLE [dbo].[HCFICHA115] (
    [ID]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION]    INT           NOT NULL,
    [TIPOTUMOR]              INT           NULL,
    [CONSULTANEO]            BIT           NULL,
    [CONSULTARECA]           BIT           NULL,
    [FECHADIAGINI]           DATE          NULL,
    [CRITERIODIAG]           INT           NULL,
    [FECHATOMA1]             DATE          NULL,
    [FECHARESUL1]            DATE          NULL,
    [CRITERIOCONF]           INT           NULL,
    [FECHATOMA2]             DATE          NULL,
    [FECHARESUL2]            DATE          NULL,
    [CODDIAGNO]              CHAR (4)      NULL,
    [FECHAINICIOTRATAMIENTO] DATE          NULL,
    [VERSION]                VARCHAR (20)  NULL,
    [JSON]                   VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA115_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA115_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA115_HCFICHANOTIFICACION1] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA115] NOCHECK CONSTRAINT [CK_HCFICHA115_JSON];


GO
ALTER TABLE [dbo].[HCFICHA115] NOCHECK CONSTRAINT [FK_HCFICHA115_HCFICHANOTIFICACION1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Criterio confirmación del diagnóstico  1=Mielograma  2=Histopatología o citología de fluido corporal  3=Inmunotipificación  4=Criterio médico especializado  5=Certificado de defunción  6=Citogenética  7=Radiología diagnóstica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'CRITERIOCONF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Criterio diagnóstico probable  1=Hemograma o extendido de sangre periférica  2=Radiología diagnóstica  3=Gammagrafía  4=Marcadores tumorales  5=Clínica sin otra ayuda diagnóstica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'CRITERIODIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consulta actual por recaída  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'CONSULTARECA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consulta actual por segunda neoplasia  True = Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'CONSULTANEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Cancer or Tumor  1:Leucemia linfoide aguda    2:Leucemia mieloide aguda  3:Otras leucemias  4:Linfomas y neoplasias  5:Tumores del sistema nervioso central  6:Neuroblastoma y otros tumores de células nerviosas periféricas  7:Retinoblastoma  8:Tumores renales  9:Tumores hepáticos  10:Tumores óseos malignos  11:Sarcomas de tejidos blandos y extra óseos  12:Tumores germinales trofoblásticos y otros gonadales  13:Tumores epiteliales malignos y melanoma  14:Otras neoplasias malignas no especificadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'TIPOTUMOR';

