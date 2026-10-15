CREATE TABLE [dbo].[HCPCEDIAGINT] (
    [IDDIAGINT]      INT IDENTITY (1, 1) NOT NULL,
    [IDVALDIAG]      INT NOT NULL,
    [CODINTPCE]      INT NOT NULL,
    [IDHCPCECONTROL] INT NOT NULL,
    CONSTRAINT [PK_HCPCEDIAGINT] PRIMARY KEY CLUSTERED ([IDDIAGINT] ASC),
    CONSTRAINT [FK_HCPCEDIAGINT_HCINTERVENPCE] FOREIGN KEY ([CODINTPCE]) REFERENCES [dbo].[HCINTERVENPCE] ([CODINTPCE]),
    CONSTRAINT [FK_HCPCEDIAGINT_HCPCEVALDIAG] FOREIGN KEY ([IDVALDIAG]) REFERENCES [dbo].[HCPCEVALDIAG] ([IDVALDIAG])
);


GO
ALTER TABLE [dbo].[HCPCEDIAGINT] NOCHECK CONSTRAINT [FK_HCPCEDIAGINT_HCINTERVENPCE];


GO
ALTER TABLE [dbo].[HCPCEDIAGINT] NOCHECK CONSTRAINT [FK_HCPCEDIAGINT_HCPCEVALDIAG];


GO
CREATE NONCLUSTERED INDEX [IX_HCPCEDIAGINT_IDHCPCECONTROL_IDVALDIAG_CODINTPCE]
    ON [dbo].[HCPCEDIAGINT]([IDHCPCECONTROL] ASC, [IDVALDIAG] ASC)
    INCLUDE([CODINTPCE]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'id de la tabla de los cambios de  cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGINT', @level2type = N'COLUMN', @level2name = N'IDHCPCECONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de la intervención plan cuidado enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGINT', @level2type = N'COLUMN', @level2name = N'CODINTPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el id valor diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGINT', @level2type = N'COLUMN', @level2name = N'IDVALDIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el diagnostico de intervencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGINT', @level2type = N'COLUMN', @level2name = N'IDDIAGINT';

