-- =====================================================
-- RDA Clarification Notes Table
-- =====================================================
-- Date: 2026-06-22
-- Description:
-- Stores clarification notes associated with submitted RDAs.
-- =====================================================

IF NOT EXISTS (
    SELECT 1
    FROM sys.schemas
    WHERE name = 'rda'
)
BEGIN
    EXEC('CREATE SCHEMA rda');
END
GO

IF OBJECT_ID('rda.rda_clarification_notes', 'U') IS NULL
BEGIN
    CREATE TABLE rda.rda_clarification_notes
    (
        -- Unique identifier
        id INT IDENTITY(1,1) NOT NULL,

        -- Related RDA identifier (GUID)
        rda_id UNIQUEIDENTIFIER NOT NULL,

        -- Related admission number
        numingres VARCHAR(20) NOT NULL,

        -- Clarification note
        note VARCHAR(500) NOT NULL,

        -- User who created the note
        created_by VARCHAR(50) NOT NULL,

        -- Healthcare professional code
        professional VARCHAR(50) NOT NULL,

        -- Creation timestamp
        created_at DATETIME NOT NULL
            CONSTRAINT DF_rda_clarification_notes_created_at
            DEFAULT GETDATE(),

        -- Audit fields
        modified_by VARCHAR(50) NULL,

        modified_at DATETIME NULL,

        CONSTRAINT pk_rda_clarification_notes
            PRIMARY KEY CLUSTERED (id ASC)
    );
END
ELSE
BEGIN
    PRINT 'Table rda.rda_clarification_notes already exists';
END
GO

-- =====================================================
-- Indexes
-- =====================================================

-- Search by RDA identifier
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'ix_rda_clarification_notes_rda_id'
      AND object_id = OBJECT_ID('rda.rda_clarification_notes')
)
BEGIN
    CREATE NONCLUSTERED INDEX ix_rda_clarification_notes_rda_id
        ON rda.rda_clarification_notes (rda_id)
        INCLUDE (numingres, created_at);
END
GO

-- Search by admission number
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'ix_rda_clarification_notes_numingres'
      AND object_id = OBJECT_ID('rda.rda_clarification_notes')
)
BEGIN
    CREATE NONCLUSTERED INDEX ix_rda_clarification_notes_numingres
        ON rda.rda_clarification_notes (numingres)
        INCLUDE (rda_id, created_at);
END
GO

-- Historical queries ordered by creation date
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'ix_rda_clarification_notes_created_at'
      AND object_id = OBJECT_ID('rda.rda_clarification_notes')
)
BEGIN
    CREATE NONCLUSTERED INDEX ix_rda_clarification_notes_created_at
        ON rda.rda_clarification_notes (created_at DESC)
        INCLUDE (rda_id, numingres, professional);
END
GO

-- Search by admission number and RDA identifier
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'ix_rda_clarification_notes_numingres_rda_id'
      AND object_id = OBJECT_ID('rda.rda_clarification_notes')
)
BEGIN
    CREATE NONCLUSTERED INDEX ix_rda_clarification_notes_numingres_rda_id
        ON rda.rda_clarification_notes (numingres, rda_id)
        INCLUDE (created_at, professional);
END
GO

-- =====================================================
-- Extended Properties (Documentation)
-- =====================================================

-- Table
GO
EXEC sp_addextendedproperty
    @name = N'MS_Description',
    @value = N'Stores clarification notes associated with submitted RDAs. Each record represents a clarification made by a healthcare professional for a specific RDA and admission.',
    @level0type = N'SCHEMA', @level0name = N'rda',
    @level1type = N'TABLE',  @level1name = N'rda_clarification_notes';
GO

-- id
EXEC sp_addextendedproperty
    @name = N'MS_Description',
    @value = N'Unique identifier of the clarification note.',
    @level0type = N'SCHEMA', @level0name = N'rda',
    @level1type = N'TABLE',  @level1name = N'rda_clarification_notes',
    @level2type = N'COLUMN', @level2name = N'id';
GO

-- rda_id
EXEC sp_addextendedproperty
    @name = N'MS_Description',
    @value = N'Unique identifier (GUID) of the RDA associated with the clarification note.',
    @level0type = N'SCHEMA', @level0name = N'rda',
    @level1type = N'TABLE',  @level1name = N'rda_clarification_notes',
    @level2type = N'COLUMN', @level2name = N'rda_id';
GO

-- numingres
EXEC sp_addextendedproperty
    @name = N'MS_Description',
    @value = N'Admission number associated with the RDA.',
    @level0type = N'SCHEMA', @level0name = N'rda',
    @level1type = N'TABLE',  @level1name = N'rda_clarification_notes',
    @level2type = N'COLUMN', @level2name = N'numingres';
GO

-- note
EXEC sp_addextendedproperty
    @name = N'MS_Description',
    @value = N'Clarification note entered by the healthcare professional.',
    @level0type = N'SCHEMA', @level0name = N'rda',
    @level1type = N'TABLE',  @level1name = N'rda_clarification_notes',
    @level2type = N'COLUMN', @level2name = N'note';
GO

-- created_by
EXEC sp_addextendedproperty
    @name = N'MS_Description',
    @value = N'User account that created the clarification note.',
    @level0type = N'SCHEMA', @level0name = N'rda',
    @level1type = N'TABLE',  @level1name = N'rda_clarification_notes',
    @level2type = N'COLUMN', @level2name = N'created_by';
GO

-- professional
EXEC sp_addextendedproperty
    @name = N'MS_Description',
    @value = N'Healthcare professional code associated with the clarification note.',
    @level0type = N'SCHEMA', @level0name = N'rda',
    @level1type = N'TABLE',  @level1name = N'rda_clarification_notes',
    @level2type = N'COLUMN', @level2name = N'professional';
GO

-- created_at
EXEC sp_addextendedproperty
    @name = N'MS_Description',
    @value = N'Date and time when the clarification note was created.',
    @level0type = N'SCHEMA', @level0name = N'rda',
    @level1type = N'TABLE',  @level1name = N'rda_clarification_notes',
    @level2type = N'COLUMN', @level2name = N'created_at';
GO

-- modified_by
EXEC sp_addextendedproperty
    @name = N'MS_Description',
    @value = N'User account that last modified the clarification note.',
    @level0type = N'SCHEMA', @level0name = N'rda',
    @level1type = N'TABLE',  @level1name = N'rda_clarification_notes',
    @level2type = N'COLUMN', @level2name = N'modified_by';
GO

-- modified_at
EXEC sp_addextendedproperty
    @name = N'MS_Description',
    @value = N'Date and time of the last modification to the clarification note.',
    @level0type = N'SCHEMA', @level0name = N'rda',
    @level1type = N'TABLE',  @level1name = N'rda_clarification_notes',
    @level2type = N'COLUMN', @level2name = N'modified_at';
GO