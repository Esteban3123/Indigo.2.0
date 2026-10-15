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

