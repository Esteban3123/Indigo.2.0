CREATE TABLE [dbo].[HCRADFIRMAS] (
    [ID]              INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADESQUEMAS] INT                                                                           NOT NULL,
    [CODPROSAL]       CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODESPECI]       CHAR (3)                                                                      NOT NULL,
    [FECHACREA]       DATETIME                                                                      NOT NULL,
    [USUCREA]         CHAR (20)                                                                     NOT NULL,
    CONSTRAINT [PK_HCRADFIRMAS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADFIRMAS_HCRADESQUEMAS] FOREIGN KEY ([IDHCRADESQUEMAS]) REFERENCES [dbo].[HCRADESQUEMAS] ([ID]),
    CONSTRAINT [FK_HCRADFIRMAS_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCRADFIRMAS_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[HCRADFIRMAS] NOCHECK CONSTRAINT [FK_HCRADFIRMAS_HCRADESQUEMAS];


GO
ALTER TABLE [dbo].[HCRADFIRMAS] NOCHECK CONSTRAINT [FK_HCRADFIRMAS_INESPECIA];


GO
ALTER TABLE [dbo].[HCRADFIRMAS] NOCHECK CONSTRAINT [FK_HCRADFIRMAS_INPROFSAL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo usuario de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADFIRMAS', @level2type = N'COLUMN', @level2name = N'USUCREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADFIRMAS', @level2type = N'COLUMN', @level2name = N'FECHACREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADFIRMAS', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo profesional que firma esquema ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADFIRMAS', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de esquemas de radioterapia (HCRADESQUEMAS)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADFIRMAS', @level2type = N'COLUMN', @level2name = N'IDHCRADESQUEMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADFIRMAS', @level2type = N'COLUMN', @level2name = N'ID';

