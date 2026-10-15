CREATE TABLE [dbo].[HCENTREGATURNOREFEREN] (
    [ID]                      INT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCENTREGATURNOC]       INT        NOT NULL,
    [IDHCENTREGATURNOCPACIEN] INT        NOT NULL,
    [NUMEFOLIO]               NCHAR (10) NOT NULL,
    [IDHCREFCONP]             INT        NOT NULL,
    [ESTADO]                  INT        NOT NULL,
    CONSTRAINT [PK_HCENTREGATURNOREFER] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCENTREGATURNOREFER_HCENTREGATURNOC] FOREIGN KEY ([IDHCENTREGATURNOC]) REFERENCES [dbo].[HCENTREGATURNOC] ([ID]),
    CONSTRAINT [FK_HCENTREGATURNOREFER_HCENTREGATURNOCPACIEN] FOREIGN KEY ([IDHCENTREGATURNOCPACIEN]) REFERENCES [dbo].[HCENTREGATURNOCPACIEN] ([ID]),
    CONSTRAINT [FK_HCENTREGATURNOREFEREN_HCREFCONP] FOREIGN KEY ([IDHCREFCONP]) REFERENCES [dbo].[HCREFCONP] ([AUTO])
);


GO
ALTER TABLE [dbo].[HCENTREGATURNOREFEREN] NOCHECK CONSTRAINT [FK_HCENTREGATURNOREFEREN_HCREFCONP];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado para saber como esta la gestion de la referencia   1->Solicitado.  2->Pendinte o Gestionando.  3->Aceptado con pediente de salida.  4->Suspendido.  5->Extramural/ ya salio. Se marca en 5 si la solicitud de referencia la marcan como tipo extramural sin importar si la Unidad funcional es de tipo consulta externa ó intrahospitalaria ´o Cuando hago una solicitud Intrahospitalaria y el paciente sale de la institución.  6->Solicitud con Pertinencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla cabecera HCREFCONP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN', @level2type = N'COLUMN', @level2name = N'IDHCREFCONP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla cabecera HCENTREGATURNOCPACIENTE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOCPACIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla cabecera HCENTREGATURNO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Autonumerico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN', @level2type = N'COLUMN', @level2name = N'ID';

