CREATE TABLE [dbo].[AGDISPONEQUIPO] (
    [ID]            INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]     CHAR (10) NOT NULL,
    [IDEQUIPO]      INT       NOT NULL,
    [FECHORAIN]     DATETIME  NOT NULL,
    [FECHORAFI]     DATETIME  NOT NULL,
    [CODUSUARIO]    CHAR (20) NOT NULL,
    [FECHACREACION] DATETIME  NOT NULL,
    [TURNOBLOQ]     BIT       NOT NULL,
    CONSTRAINT [PK_AGDISPONEQUIPO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGDISPONEQUIPO_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_AGDISPONEQUIPO_AGEQUIPTRA] FOREIGN KEY ([IDEQUIPO]) REFERENCES [dbo].[AGEQUIPTRA] ([ID])
);


GO
ALTER TABLE [dbo].[AGDISPONEQUIPO] NOCHECK CONSTRAINT [FK_AGDISPONEQUIPO_ADCENATEN];


GO
ALTER TABLE [dbo].[AGDISPONEQUIPO] NOCHECK CONSTRAINT [FK_AGDISPONEQUIPO_AGEQUIPTRA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disponibilidad bloqueada:  1: si  0: no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONEQUIPO', @level2type = N'COLUMN', @level2name = N'TURNOBLOQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONEQUIPO', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo usuario que crea registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONEQUIPO', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y hora Final', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONEQUIPO', @level2type = N'COLUMN', @level2name = N'FECHORAFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y hora inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONEQUIPO', @level2type = N'COLUMN', @level2name = N'FECHORAIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id equipo de tratamiento especial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONEQUIPO', @level2type = N'COLUMN', @level2name = N'IDEQUIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONEQUIPO', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONEQUIPO', @level2type = N'COLUMN', @level2name = N'ID';

