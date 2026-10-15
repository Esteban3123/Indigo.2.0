CREATE TABLE [dbo].[CHCAMASHO] (
    [CODICAMAS]          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMCAMHOS]          CHAR (10)    NOT NULL,
    [DESCCAMAS]          CHAR (50)    NOT NULL,
    [CODCENATE]          CHAR (10)    NOT NULL,
    [UFUCODIGO]          CHAR (10)    NOT NULL,
    [CODCLAHAB]          INT          NOT NULL,
    [CODCLACAM]          INT          NOT NULL,
    [ESTADCAMA]          INT          NOT NULL,
    [CODAISLAM]          INT          NULL,
    [CAMVIRTUA]          BIT          NOT NULL,
    [CAMTRACIR]          BIT          NOT NULL,
    [CAMTRAMED]          BIT          NOT NULL,
    [CAMDEVMED]          BIT          NOT NULL,
    [CAMTIPANO]          BIT          NOT NULL,
    [CODCONCEC]          NUMERIC (18) NULL,
    [CAMRECMED]          BIT          NOT NULL,
    [CAMRECDEV]          BIT          NOT NULL,
    [BedType]            INT          NULL,
    [TypeTransfer]       INT          NULL,
    [ConcurrencyControl] ROWVERSION   NULL,
    CONSTRAINT [PK_CHCAMASHO_1] PRIMARY KEY CLUSTERED ([CODICAMAS] ASC),
    CONSTRAINT [FK_CHCAMASHO_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]) ON UPDATE CASCADE,
    CONSTRAINT [FK_CHCAMASHO_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]) ON UPDATE CASCADE
);


GO
ALTER TABLE [dbo].[CHCAMASHO] NOCHECK CONSTRAINT [FK_CHCAMASHO_ADcenaten];


GO
ALTER TABLE [dbo].[CHCAMASHO] NOCHECK CONSTRAINT [FK_CHCAMASHO_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_CHCAMASHO__NUMCAMHOS]
    ON [dbo].[CHCAMASHO]([NUMCAMHOS] ASC);


GO
CREATE TRIGGER trg_UpdateCHCAMASHO
ON CHCAMASHO
AFTER INSERT, UPDATE
AS
BEGIN
    UPDATE CHCAMASHO
    SET BedType = NULL, TypeTransfer = NULL
    WHERE ESTADCAMA = '1' 
    AND (TypeTransfer IS NOT NULL OR BedType IS NOT NULL);
END;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de concurrencia que se utiliza para validar cuando se están realizando traslados de camas de manera simultánea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'ConcurrencyControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de traslado: Columna que me especifica cuando se realiza un traslado al paciente de que tipo fue dicho traslado:

1 - Traslado con devolutivos
2 - Traslado con medicamentos
3 - Ninguno

Columna que se debe resetear a NULL 

', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'TypeTransfer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo para identificar si la cama es de origen o de destino cuando se presenta la doble estancia al hacer traslado con la opción ninguno: 
 
1 - Cama Origen
2 - Cama Destino
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'BedType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tipo de Aislamiento:  1: Aerosol  2: Contacto  3: Estandar  4: Gota  5: Protector', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CODAISLAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la Cama: 
1: Libre 
2: Asignada
3: Inactiva  
4: En Mantenimiento  
5: En Aislamiento  
6: Reservada sin Confirmar  
7: Reservada Confirmada
8: Asignada con reserva

 Nota: El estado 1, y 6 permite asignar la habitacion, los demas estados evitaran que la habitacion este disponible. El estado 6 no se registra en esta tabla solo se describe a nivel informativo [Valor no valido].', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'ESTADCAMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clase de Cama:  1: Observacion Urgencias  2: Recuperacion Post-Quirurgico  3: Hospitalaria  4:Cuna de Observación    Nota: Este campo sirve para identificar como se va a realizar el cobro de la estancia, ejemplo si la cama es de recuperacion esta no liquidara nada, si es de urgencias tendra en cuenta el parametro del Plan de Beneficios para liquidar estancia o si es Hospitalaria liquidara normal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CODCLACAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clase de la habitacion:  1: Sala de Observacion  2: Sala de Procedimientos  3: Sala de Recuperacion  4: Habitacion 1 Cama  5: Habitacion 2 Camas  6: Habitacion 3 Camas  7: Habitacion 4 Camas  8: Suite  9: Habitacion Especial  10: UCI  11: Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CODCLAHAB';

