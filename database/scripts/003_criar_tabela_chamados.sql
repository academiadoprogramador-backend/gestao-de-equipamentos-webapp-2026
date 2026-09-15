CREATE TABLE dbo.TBChamados
(
    Id INT IDENTITY(1, 1) NOT NULL,
    Titulo NVARCHAR(100) NOT NULL,
    Descricao NVARCHAR(1000) NOT NULL,
    EquipamentoId INT NOT NULL,
    DataAbertura DATE NOT NULL,

    CONSTRAINT PK_TBChamados PRIMARY KEY (Id),
    CONSTRAINT CK_TBChamados_Titulo CHECK (LEN(LTRIM(RTRIM(Titulo))) BETWEEN 1 AND 100),
    CONSTRAINT CK_TBChamados_Descricao CHECK (LEN(LTRIM(RTRIM(Descricao))) BETWEEN 1 AND 1000),
    CONSTRAINT FK_TBChamados_TBEquipamentos FOREIGN KEY (EquipamentoId)
        REFERENCES dbo.TBEquipamentos (Id)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION
);

CREATE INDEX IX_TBChamados_EquipamentoId
    ON dbo.TBChamados (EquipamentoId);
