REM  Oracle.LinuxCompatibility.MySQL.CodeSolution.VisualBasic.CodeGenerator
REM  MYSQL Schema Mapper
REM      for Microsoft VisualBasic.NET 2.1.0.2569

REM  Dump @2019/9/5 12:56:55


Imports System.Data.Linq.Mapping
Imports System.Xml.Serialization
Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes
Imports MySqlScript = Oracle.LinuxCompatibility.MySQL.Scripting.Extensions

Namespace mysql

''' <summary>
''' ```SQL
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("cas_registry", Database:="pubchem", SchemaSQL:="
CREATE TABLE IF NOT EXISTS `pubchem`.`cas_registry` (
  `cid` INT NOT NULL,
  `cas_number` VARCHAR(64) NOT NULL,
  PRIMARY KEY (`cid`))
ENGINE = InnoDB;
")>
Public Class cas_registry: Inherits Oracle.LinuxCompatibility.MySQL.MySQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("cid"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "11"), Column(Name:="cid"), XmlAttribute> Public Property cid As Long
    <DatabaseField("cas_number"), NotNull, DataType(MySqlDbType.VarChar, "64"), Column(Name:="cas_number")> Public Property cas_number As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Friend Shared ReadOnly INSERT_SQL$ = 
        <SQL>INSERT INTO `cas_registry` (`cid`, `cas_number`) VALUES ('{0}', '{1}');</SQL>

    Friend Shared ReadOnly INSERT_AI_SQL$ = 
        <SQL>INSERT INTO `cas_registry` (`cid`, `cas_number`) VALUES ('{0}', '{1}');</SQL>

    Friend Shared ReadOnly REPLACE_SQL$ = 
        <SQL>REPLACE INTO `cas_registry` (`cid`, `cas_number`) VALUES ('{0}', '{1}');</SQL>

    Friend Shared ReadOnly REPLACE_AI_SQL$ = 
        <SQL>REPLACE INTO `cas_registry` (`cid`, `cas_number`) VALUES ('{0}', '{1}');</SQL>

    Friend Shared ReadOnly DELETE_SQL$ =
        <SQL>DELETE FROM `cas_registry` WHERE `cid` = '{0}';</SQL>

    Friend Shared ReadOnly UPDATE_SQL$ = 
        <SQL>UPDATE `cas_registry` SET `cid`='{0}', `cas_number`='{1}' WHERE `cid` = '{2}';</SQL>

#End Region

''' <summary>
''' ```SQL
''' DELETE FROM `cas_registry` WHERE `cid` = '{0}';
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, cid)
    End Function

''' <summary>
''' ```SQL
''' INSERT INTO `cas_registry` (`cid`, `cas_number`) VALUES ('{0}', '{1}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, cid, cas_number)
    End Function

''' <summary>
''' ```SQL
''' INSERT INTO `cas_registry` (`cid`, `cas_number`) VALUES ('{0}', '{1}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL(AI As Boolean) As String
        If AI Then
        Return String.Format(INSERT_AI_SQL, cid, cas_number)
        Else
        Return String.Format(INSERT_SQL, cid, cas_number)
        End If
    End Function

''' <summary>
''' <see cref="GetInsertSQL"/>
''' </summary>
    Public Overrides Function GetDumpInsertValue(AI As Boolean) As String
        If AI Then
            Return $"('{cid}', '{cas_number}')"
        Else
            Return $"('{cid}', '{cas_number}')"
        End If
    End Function


''' <summary>
''' ```SQL
''' REPLACE INTO `cas_registry` (`cid`, `cas_number`) VALUES ('{0}', '{1}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, cid, cas_number)
    End Function

''' <summary>
''' ```SQL
''' REPLACE INTO `cas_registry` (`cid`, `cas_number`) VALUES ('{0}', '{1}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL(AI As Boolean) As String
        If AI Then
        Return String.Format(REPLACE_AI_SQL, cid, cas_number)
        Else
        Return String.Format(REPLACE_SQL, cid, cas_number)
        End If
    End Function

''' <summary>
''' ```SQL
''' UPDATE `cas_registry` SET `cid`='{0}', `cas_number`='{1}' WHERE `cid` = '{2}';
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, cid, cas_number, cid)
    End Function
#End Region

''' <summary>
                     ''' Memberwise clone of current table Object.
                     ''' </summary>
                     Public Function Clone() As cas_registry
                         Return DirectCast(MyClass.MemberwiseClone, cas_registry)
                     End Function
End Class


End Namespace
