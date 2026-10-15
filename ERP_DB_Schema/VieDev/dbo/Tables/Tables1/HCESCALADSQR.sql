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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ítem 1 al 20:  = 11 puntos - Alta probabilidad de sufrir un trastorno depresivo o ansioso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR', @level2type = N'COLUMN', @level2name = N'RESULTADO1';

