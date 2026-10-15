CREATE TABLE [dbo].[HCFICHA228] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [D16]                 INT           NULL,
    [D15]                 INT           NULL,
    [D13]                 INT           NULL,
    [D12]                 INT           NULL,
    [D11]                 INT           NULL,
    [D21]                 INT           NULL,
    [D22]                 INT           NULL,
    [D23]                 INT           NULL,
    [D25]                 INT           NULL,
    [D26]                 INT           NULL,
    [D46]                 INT           NULL,
    [D36]                 INT           NULL,
    [CLASIFCLIN]          INT           NULL,
    [PRESECARI]           BIT           NULL,
    [TIPCARIE]            BIT           NULL,
    [FUENTECONS]          INT           NULL,
    [INGESTCREM]          BIT           NULL,
    [INGESTENJUA]         BIT           NULL,
    [APLITOP]             BIT           NULL,
    [PERSORECLAC]         INT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA228] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA228_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA228_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA228] NOCHECK CONSTRAINT [CK_HCFICHA228_JSON];


GO
ALTER TABLE [dbo].[HCFICHA228] NOCHECK CONSTRAINT [FK_HCFICHA228_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'La Persona Recibió Lactancia Materna Exclusiva hasta los 6 meses (Solo personas que acuden con su madre a consulta)  1=si  2=no  3=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'PERSORECLAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aplicaciones Tópicas de Flúor en el ultimo año  True = Si False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'APLITOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ingesta de Enjuague Bucal  True = Si False = NO    --- (Campo Obsoleto desde version V01_2020-03-06 )', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'INGESTENJUA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ingesta de Crema Dental  True = Si   False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'INGESTCREM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fuente de Consumo de Agua (Sólo niños de6 años y gestantes)  1=Acueducto  2=Pozo Subterráneo/Aljibe  3=Quebrada  4=Agua Embotellada  5=Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'FUENTECONS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Caries  True = activa  Fasle = Inactiva    --- (Campo Obsoleto desde version V01_2020-03-06 )', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'TIPCARIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Presencia de Caries  True = si   False = No    --- (Campo Obsoleto desde version V01_2020-03-06 )', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'PRESECARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificación Clínica de las lesiones (Índice de Dean)  0=Normal1=Dudoso2=Muy Leve3=Leve4=Moderada5=Severa9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'CLASIFCLIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  0=Normal  1=Dudoso  2=Muy Leve  3=Leve  4=Moderada  5=Severa  9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D36';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  0=Normal  1=Dudoso  2=Muy Leve  3=Leve  4=Moderada  5=Severa  9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D46';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  0=Normal  1=Dudoso  2=Muy Leve  3=Leve  4=Moderada  5=Severa  9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D26';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  0=Normal  1=Dudoso  2=Muy Leve  3=Leve  4=Moderada  5=Severa  9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D25';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  0=Normal  1=Dudoso  2=Muy Leve  3=Leve  4=Moderada  5=Severa  9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D23';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  0=Normal  1=Dudoso  2=Muy Leve  3=Leve  4=Moderada  5=Severa  9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D22';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  0=Normal  1=Dudoso  2=Muy Leve  3=Leve  4=Moderada  5=Severa  9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D21';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  0=Normal  1=Dudoso  2=Muy Leve  3=Leve  4=Moderada  5=Severa  9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  0=Normal  1=Dudoso  2=Muy Leve  3=Leve  4=Moderada  5=Severa  9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D12';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  0=Normal  1=Dudoso  2=Muy Leve  3=Leve  4=Moderada  5=Severa  9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D13';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  0=Normal  1=Dudoso  2=Muy Leve  3=Leve  4=Moderada  5=Severa  9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D15';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Datos Especificos  0=Normal  1=Dudoso  2=Muy Leve  3=Leve  4=Moderada  5=Severa  9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D16';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'ID';

