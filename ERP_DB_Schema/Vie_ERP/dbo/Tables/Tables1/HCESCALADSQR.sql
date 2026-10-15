CREATE TABLE [dbo].[HCESCALADSQR] (
    [ID]          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCESCALAS] INT NOT NULL,
    [RESULTADO1]  INT NOT NULL,
    [RESULTADO2]  INT NOT NULL,
    [RESULTADO3]  INT NOT NULL,
    [RESULTADO4]  INT NOT NULL,
    CONSTRAINT [PK_HCESCALADSQR] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCESCALADSQR_HCESCALAS] FOREIGN KEY ([IDHCESCALAS]) REFERENCES [dbo].[HCESCALAS] ([ID])
);


GO
ALTER TABLE [dbo].[HCESCALADSQR] NOCHECK CONSTRAINT [FK_HCESCALADSQR_HCESCALAS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ítem 26 al 30: Una respuesta positiva indica presencia de  consumo problemático de alcohol.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR', @level2type = N'COLUMN', @level2name = N'RESULTADO4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ítem 25: Respuesta positiva en este ítem indica caso de trastorno convulsivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR', @level2type = N'COLUMN', @level2name = N'RESULTADO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ítem 21 al 24:  Una respuesta positiva indica presencia de trastorno psicótico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR', @level2type = N'COLUMN', @level2name = N'RESULTADO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ítem 1 al 20:  = 11 puntos - Alta probabilidad de sufrir un trastorno depresivo o ansioso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR', @level2type = N'COLUMN', @level2name = N'RESULTADO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relación con la tabla HCESCALAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR', @level2type = N'COLUMN', @level2name = N'IDHCESCALAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR', @level2type = N'COLUMN', @level2name = N'ID';

