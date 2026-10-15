CREATE TABLE [dbo].[CHREGESTA] (
    [IPCODPACI]   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]   CHAR (10)                                                                        NOT NULL,
    [CODICAMAS]   INT                                                                              NOT NULL,
    [FECINIEST]   DATETIME                                                                         NOT NULL,
    [FECFINEST]   DATETIME                                                                         NOT NULL,
    [CODTIPEST]   CHAR (3)                                                                         NOT NULL,
    [REINGRESO]   BIT                                                                              NOT NULL,
    [TIPINGEST]   CHAR (2)                                                                         NOT NULL,
    [TIPREIUNI]   BIT                                                                              NOT NULL,
    [REGESTADO]   INT                                                                              NOT NULL,
    [REGDIAEST]   INT                                                                              NULL,
    [REGUSUARI]   CHAR (20)                                                                        NULL,
    [FECREGSIS]   DATETIME                                                                         CONSTRAINT [DF_CHREGESTA_FECREGSIS] DEFAULT ([Common].[getdate]()) NOT NULL,
    [INDAUDFOR]   NUMERIC (18)                                                                     NOT NULL,
    [JUSCAMANT]   VARCHAR (4000)                                                                   NULL,
    [ID]          INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GENESTLIQ]   TINYINT                                                                          NULL,
    [NOREQAUTO]   BIT                                                                              NULL,
    [NOREQAUTJUS] VARCHAR (4000)                                                                   NULL,
    [IDAGEPROGQX] INT                                                                              NULL,
    [CODPROSAL]   CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [CODESPECI]   CHAR (3)                                                                         NULL,
    CONSTRAINT [PK_CHREGESTA_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CHREGESTA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_CHREGESTA_CHCAMASHO] FOREIGN KEY ([CODICAMAS]) REFERENCES [dbo].[CHCAMASHO] ([CODICAMAS]),
    CONSTRAINT [FK_CHREGESTA_CHTIPESTA] FOREIGN KEY ([CODTIPEST]) REFERENCES [dbo].[CHTIPESTA] ([CODTIPEST]),
    CONSTRAINT [FK_CHREGESTA_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_CHREGESTA_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_CHREGESTA_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[CHREGESTA] NOCHECK CONSTRAINT [FK_CHREGESTA_ADINGRESO];


GO
ALTER TABLE [dbo].[CHREGESTA] NOCHECK CONSTRAINT [FK_CHREGESTA_CHCAMASHO];


GO
ALTER TABLE [dbo].[CHREGESTA] NOCHECK CONSTRAINT [FK_CHREGESTA_CHTIPESTA];


GO
ALTER TABLE [dbo].[CHREGESTA] NOCHECK CONSTRAINT [FK_CHREGESTA_INESPECIA];


GO
ALTER TABLE [dbo].[CHREGESTA] NOCHECK CONSTRAINT [FK_CHREGESTA_INPacient];


GO
ALTER TABLE [dbo].[CHREGESTA] NOCHECK CONSTRAINT [FK_CHREGESTA_INPROFSAL];


GO
CREATE NONCLUSTERED INDEX [IX_CHREGESTA]
    ON [dbo].[CHREGESTA]([IPCODPACI] ASC, [NUMINGRES] ASC);


GO
CREATE NONCLUSTERED INDEX [_dta_index_CHREGESTA_7_1256391545__K3_K10_K2_K1]
    ON [dbo].[CHREGESTA]([CODICAMAS] ASC, [REGESTADO] ASC, [NUMINGRES] ASC, [IPCODPACI] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CHREGESTA_CODICAMAS_FECINIEST_FECFINEST_CODTIPEST_INDAUDFOR_IPCODPACI_NUMINGRES_REGDIAEST_REGESTADO_REGUSUARI_REINGRESO]
    ON [dbo].[CHREGESTA]([CODICAMAS] ASC, [FECINIEST] ASC, [FECFINEST] ASC)
    INCLUDE([CODTIPEST], [INDAUDFOR], [IPCODPACI], [NUMINGRES], [REGDIAEST], [REGESTADO], [REGUSUARI], [REINGRESO], [TIPINGEST], [TIPREIUNI]);


GO
CREATE NONCLUSTERED INDEX [IDX_RegistroEstancia]
    ON [dbo].[CHREGESTA]([CODICAMAS] ASC, [REGESTADO] ASC)
    INCLUDE([CODTIPEST], [IPCODPACI], [NUMINGRES]);


GO
CREATE NONCLUSTERED INDEX [IX_CHREGESTA_CODICAMAS_FECINIEST_FECFINEST_CODTIPEST]
    ON [dbo].[CHREGESTA]([CODICAMAS] ASC, [FECINIEST] ASC, [FECFINEST] ASC)
    INCLUDE([CODTIPEST], [ID], [INDAUDFOR], [IPCODPACI], [NUMINGRES], [REGDIAEST], [REGESTADO], [REGUSUARI], [REINGRESO], [TIPINGEST], [TIPREIUNI]);


GO
CREATE NONCLUSTERED INDEX [IX_CHREGESTA_1]
    ON [dbo].[CHREGESTA]([NUMINGRES] ASC, [CODICAMAS] ASC, [FECINIEST] ASC, [CODTIPEST] ASC, [REGESTADO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_NUMINGRESRIPS]
    ON [dbo].[CHREGESTA]([NUMINGRES] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CHREGESTA_REGESTADO_CODICAMAS_IPCODPACI_NUMINGRES]
    ON [dbo].[CHREGESTA]([REGESTADO] ASC)
    INCLUDE([CODICAMAS], [IPCODPACI], [NUMINGRES]);


GO
CREATE NONCLUSTERED INDEX [IX_CHREGESTA_NUMINGRES]
    ON [dbo].[CHREGESTA]([NUMINGRES] ASC)
    INCLUDE([CODICAMAS], [FECFINEST]);


GO
CREATE NONCLUSTERED INDEX [IX_CHREGESTA_CODICAMAS_FECINIEST_FECFINEST_NUMINGRES]
    ON [dbo].[CHREGESTA]([CODICAMAS] ASC, [FECINIEST] ASC)
    INCLUDE([FECFINEST], [NUMINGRES]);


GO
CREATE TRIGGER [dbo].[tgg_ActualizarBedType_TypeTransfer]
   ON  [dbo].[CHREGESTA]
   AFTER INSERT, UPDATE
AS 
BEGIN

	declare @Paciente as varchar(25) = (select top 1 IPCODPACI   from inserted)
	declare @Ingreso as varchar(10) = (select top 1 NUMINGRES  from inserted)
	declare @Estado as integer = (select top 1 REGESTADO  from inserted)

SET NOCOUNT ON;


if @Estado = 2 begin 
	
	-- Insert the bed ids from the inserted or updated records in CHREGESTA into the temporary table
    CREATE TABLE #TablaEstanacia (CodigoCama INT);
	INSERT INTO #TablaEstanacia (CodigoCama)
    SELECT distinct A.CODICAMAS FROM CHREGESTA A INNER JOIN CHCAMASHO B ON A.CODICAMAS = B.CODICAMAS where B.ESTADCAMA IN (1,2) AND  IPCODPACI = @Paciente and NUMINGRES = @Ingreso


    -- Update the status in CHCAMASHO based on the bed ids from the temporary table
    UPDATE CHCAMASHO
    SET BedType = null, TypeTransfer = null 
    FROM CHCAMASHO c
    INNER JOIN #TablaEstanacia t ON c.CODICAMAS  = t.CodigoCama;

    -- Clean up the temporary table
    DROP TABLE #TablaEstanacia;
END

END
GO
DISABLE TRIGGER [dbo].[tgg_ActualizarBedType_TypeTransfer]
    ON [dbo].[CHREGESTA];


GO
Create trigger [dbo].[DetectarDobleRegistroHSAP]
on [dbo].[CHREGESTA]
for insert as
begin
    declare @Ingreso as varchar(10) = (select  NUMINGRES  from inserted)
	declare @Identificacion as varchar(10) = (select IPCODPACI    from inserted)
			
	   declare @ExisteDiagnosticoenIndiagnop int
			select @ExisteDiagnosticoenIndiagnop = count(*) from CHREGESTA where NUMINGRES = @Ingreso AND IPCODPACI   = @Identificacion  AND REGESTADO = 1

			if @ExisteDiagnosticoenIndiagnop > 1 begin
				  raiserror('Contacte administrador sistemas: no puede llegar Grabar dos registro activos :)!',10,1)
	              rollback transaction
			end
end
GO
DISABLE TRIGGER [dbo].[DetectarDobleRegistroHSAP]
    ON [dbo].[CHREGESTA];


GO
CREATE TRIGGER [dbo].[UPDATE_CODPROSAL] 
ON [dbo].[CHREGESTA] AFTER INSERT AS  

IF((SELECT COUNT(*) FROM INSERTED i where i.CODPROSAL IS NULL) > 0) BEGIN 	
	UPDATE CHREGESTA 
		SET CODPROSAL = '999', CODESPECI = '002'
	where ID = (SELECT ID FROM INSERTED)
END;
GO
CREATE TRIGGER trg_UpdateADIngreso
ON CHREGESTA
AFTER UPDATE
AS
BEGIN
    UPDATE ADINGRESO
    SET ADINGRESO.CODCAMACT = i.CODICAMAS
    FROM ADINGRESO b
    INNER JOIN INSERTED i ON b.NUMINGRES = i.NUMINGRES
    WHERE i.REGESTADO = 1 AND i.CODICAMAS <> b.CODCAMACT;
END;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la Especialidad Tratante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del Médico Tratante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la Tabla donde se guardan las programaiones de cirugía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'NOREQAUTJUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Actualizar el No requiere autorización  1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'NOREQAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Liquidacion de Estancia VIE  1. Sin Liquidar  2. Liquidada Parcial  3. Liquidada Total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'GENESTLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificacion cambio estancia fecha anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'JUSCAMANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha que se Registra en el Sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que Crea el Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'REGUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Dias de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'REGDIAEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Registro:  1: Activo  2: Pendiente Liquidar  3: Liquidado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'REGESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Reingreso a la Unidad  True-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'TIPREIUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Ingreso Estancia:  NU -> Nuevo Unidad  TC -> Traslado Interno de Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'TIPINGEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Reingreso de Paciente a la IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'REINGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tipo de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'CODTIPEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'FECFINEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'FECINIEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'CODICAMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';

