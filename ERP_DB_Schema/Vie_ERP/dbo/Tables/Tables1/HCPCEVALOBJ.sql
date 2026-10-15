CREATE TABLE [dbo].[HCPCEVALOBJ] (
    [IDVALOBJ]       INT IDENTITY (1, 1) NOT NULL,
    [IDPLANVAL]      INT NOT NULL,
    [CODOBJPCE]      INT NOT NULL,
    [IDHCPCECONTROL] INT NOT NULL,
    CONSTRAINT [PK_HCPCEVALOBJ] PRIMARY KEY CLUSTERED ([IDVALOBJ] ASC),
    CONSTRAINT [FK_HCPCEVALOBJ_HCOBJPCE] FOREIGN KEY ([CODOBJPCE]) REFERENCES [dbo].[HCOBJPCE] ([CODOBJPCE]),
    CONSTRAINT [FK_HCPCEVALOBJ_HCPCEPLANVAL] FOREIGN KEY ([IDPLANVAL]) REFERENCES [dbo].[HCPCEPLANVAL] ([IDPLANVAL])
);


GO
ALTER TABLE [dbo].[HCPCEVALOBJ] NOCHECK CONSTRAINT [FK_HCPCEVALOBJ_HCOBJPCE];


GO
ALTER TABLE [dbo].[HCPCEVALOBJ] NOCHECK CONSTRAINT [FK_HCPCEVALOBJ_HCPCEPLANVAL];


GO
CREATE NONCLUSTERED INDEX [IX_HCPCEVALOBJ_IDHCPCECONTROL]
    ON [dbo].[HCPCEVALOBJ]([IDHCPCECONTROL] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el id del control del plan de cuidado enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALOBJ', @level2type = N'COLUMN', @level2name = N'IDHCPCECONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo objetos de planes  de cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALOBJ', @level2type = N'COLUMN', @level2name = N'CODOBJPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id  del Identificador del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALOBJ', @level2type = N'COLUMN', @level2name = N'IDPLANVAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id valores objetos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALOBJ', @level2type = N'COLUMN', @level2name = N'IDVALOBJ';

