CREATE TABLE [dbo].[AGFARMEONCOC] (
    [ID]         INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCITA]     INT                                                                              NOT NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODUSUARIO] CHAR (20)                                                                        NOT NULL,
    [CODPROSAL]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [JUSANULAC]  VARCHAR (250)                                                                    NULL,
    [ESTADO]     INT                                                                              NOT NULL,
    [FECHAORDE]  DATETIME                                                                         NOT NULL,
    CONSTRAINT [PK_AGFARMEONCOC__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGFARMEONCOC_AGASICITA] FOREIGN KEY ([IDCITA]) REFERENCES [dbo].[AGASICITA] ([CODAUTONU]),
    CONSTRAINT [FK_AGFARMEONCOC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_AGFARMEONCOC_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[AGFARMEONCOC] NOCHECK CONSTRAINT [FK_AGFARMEONCOC_AGASICITA];


GO
ALTER TABLE [dbo].[AGFARMEONCOC] NOCHECK CONSTRAINT [FK_AGFARMEONCOC_INPACIENT];


GO
ALTER TABLE [dbo].[AGFARMEONCOC] NOCHECK CONSTRAINT [FK_AGFARMEONCOC_INPROFSAL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la Orden de solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOC', @level2type = N'COLUMN', @level2name = N'FECHAORDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Orden:  1. Pendiente  2. Entregada  3. Anulada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOC', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificación Anulacion de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOC', @level2type = N'COLUMN', @level2name = N'JUSANULAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Profesional solicita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Usuario Solicita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOC', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Cita que realiza la solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOC', @level2type = N'COLUMN', @level2name = N'IDCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la Solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOC', @level2type = N'COLUMN', @level2name = N'ID';

