CREATE TABLE [dbo].[AGASICITACONFIR] (
    [ID]                  INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDAGASICITA]         INT                                                                              NULL,
    [IPCODPACI]           VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [ESTADOCONFIRMAR]     CHAR (1)                                                                         NOT NULL,
    [FECHAHORA]           DATETIME                                                                         NOT NULL,
    [CONTACTOPACIENTE]    BIT                                                                              NOT NULL,
    [NOMBRECONTACTO]      VARCHAR (80)                                                                     NULL,
    [PARENTESCO]          VARCHAR (80)                                                                     NULL,
    [OBSERVACION]         VARCHAR (500)                                                                    NULL,
    [FECHACREACION]       DATETIME                                                                         NOT NULL,
    [USUARIOCREACION]     CHAR (20)                                                                        NOT NULL,
    [FECHAMODIFICACION]   DATETIME                                                                         NULL,
    [USUARIOMODIFICACION] CHAR (20)                                                                        NULL,
    [IDAGEPROGQX]         INT                                                                              NULL,
    CONSTRAINT [PK_AGASICITACONFIR] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGASICITACONFIR_AGASICITA] FOREIGN KEY ([IDAGASICITA]) REFERENCES [dbo].[AGASICITA] ([CODAUTONU]),
    CONSTRAINT [FK_AGASICITACONFIR_AGEPROGQX] FOREIGN KEY ([IDAGEPROGQX]) REFERENCES [dbo].[AGEPROGQX] ([CODAUTONU]),
    CONSTRAINT [FK_AGASICITACONFIR_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[AGASICITACONFIR] NOCHECK CONSTRAINT [FK_AGASICITACONFIR_AGASICITA];


GO
ALTER TABLE [dbo].[AGASICITACONFIR] NOCHECK CONSTRAINT [FK_AGASICITACONFIR_AGEPROGQX];


GO
ALTER TABLE [dbo].[AGASICITACONFIR] NOCHECK CONSTRAINT [FK_AGASICITACONFIR_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID o Codigo Autonumerico de la tabla AGEPROGQX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parentesco del contacto con el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'PARENTESCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'NOMBRECONTACTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contacto al Paciente ? --> 0=No , 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'CONTACTOPACIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y Hora de Consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'FECHAHORA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de Confirmacion de Asistencia --> 1= Confirmado , 2 =Cancelado , 3= Sin Definir', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'ESTADOCONFIRMAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID o Codigo Autonumerico de la tabla AGASICITA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'IDAGASICITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'ID';

