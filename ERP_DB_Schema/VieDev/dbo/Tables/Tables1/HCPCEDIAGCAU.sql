CREATE TABLE [dbo].[HCPCEDIAGCAU] (
    [IDDIAGCAU]      INT IDENTITY (1, 1) NOT NULL,
    [IDVALDIAG]      INT NOT NULL,
    [CODCAUSAPCE]    INT NOT NULL,
    [IDHCPCECONTROL] INT NOT NULL,
    CONSTRAINT [PK_HCPCEDIAGCAU] PRIMARY KEY CLUSTERED ([IDDIAGCAU] ASC),
    CONSTRAINT [FK_HCPCEDIAGCAU_HCCAUSAPCE] FOREIGN KEY ([CODCAUSAPCE]) REFERENCES [dbo].[HCCAUSAPCE] ([CODCAUSAPCE]),
    CONSTRAINT [FK_HCPCEDIAGCAU_HCPCEVALDIAG] FOREIGN KEY ([IDVALDIAG]) REFERENCES [dbo].[HCPCEVALDIAG] ([IDVALDIAG])
);


GO
ALTER TABLE [dbo].[HCPCEDIAGCAU] NOCHECK CONSTRAINT [FK_HCPCEDIAGCAU_HCCAUSAPCE];


GO
ALTER TABLE [dbo].[HCPCEDIAGCAU] NOCHECK CONSTRAINT [FK_HCPCEDIAGCAU_HCPCEVALDIAG];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el id del control del plan de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGCAU', @level2type = N'COLUMN', @level2name = N'IDHCPCECONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  Código de la causa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGCAU', @level2type = N'COLUMN', @level2name = N'CODCAUSAPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el id valor diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGCAU', @level2type = N'COLUMN', @level2name = N'IDVALDIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Id del daignostico de la causa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGCAU', @level2type = N'COLUMN', @level2name = N'IDDIAGCAU';

