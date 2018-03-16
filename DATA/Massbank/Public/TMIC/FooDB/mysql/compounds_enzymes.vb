REM  Oracle.LinuxCompatibility.MySQL.CodeSolution.VisualBasic.CodeGenerator
REM  MYSQL Schema Mapper
REM      for Microsoft VisualBasic.NET 2.1.0.2569

REM  Dump @2018/3/16 13:47:47


Imports System.Data.Linq.Mapping
Imports System.Xml.Serialization
Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes
Imports MySqlScript = Oracle.LinuxCompatibility.MySQL.Scripting.Extensions

Namespace TMIC.FooDB.mysql

''' <summary>
''' ```SQL
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `compounds_enzymes`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `compounds_enzymes` (
'''   `id` int(11) NOT NULL AUTO_INCREMENT,
'''   `compound_id` int(11) NOT NULL,
'''   `enzyme_id` int(11) NOT NULL,
'''   `citations` mediumtext COLLATE utf8_unicode_ci NOT NULL,
'''   `created_at` datetime DEFAULT NULL,
'''   `updated_at` datetime DEFAULT NULL,
'''   `creator_id` int(11) DEFAULT NULL,
'''   `updater_id` int(11) DEFAULT NULL,
'''   PRIMARY KEY (`id`),
'''   UNIQUE KEY `index_compounds_enzymes_on_compound_id_and_enzyme_id` (`compound_id`,`enzyme_id`)
''' ) ENGINE=InnoDB AUTO_INCREMENT=105090 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("compounds_enzymes", Database:="foodb", SchemaSQL:="
CREATE TABLE `compounds_enzymes` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `compound_id` int(11) NOT NULL,
  `enzyme_id` int(11) NOT NULL,
  `citations` mediumtext COLLATE utf8_unicode_ci NOT NULL,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `creator_id` int(11) DEFAULT NULL,
  `updater_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `index_compounds_enzymes_on_compound_id_and_enzyme_id` (`compound_id`,`enzyme_id`)
) ENGINE=InnoDB AUTO_INCREMENT=105090 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;")>
Public Class compounds_enzymes: Inherits Oracle.LinuxCompatibility.MySQL.MySQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("id"), PrimaryKey, AutoIncrement, NotNull, DataType(MySqlDbType.Int64, "11"), Column(Name:="id"), XmlAttribute> Public Property id As Long
    <DatabaseField("compound_id"), NotNull, DataType(MySqlDbType.Int64, "11"), Column(Name:="compound_id")> Public Property compound_id As Long
    <DatabaseField("enzyme_id"), NotNull, DataType(MySqlDbType.Int64, "11"), Column(Name:="enzyme_id")> Public Property enzyme_id As Long
    <DatabaseField("citations"), NotNull, DataType(MySqlDbType.Text), Column(Name:="citations")> Public Property citations As String
    <DatabaseField("created_at"), DataType(MySqlDbType.DateTime), Column(Name:="created_at")> Public Property created_at As Date
    <DatabaseField("updated_at"), DataType(MySqlDbType.DateTime), Column(Name:="updated_at")> Public Property updated_at As Date
    <DatabaseField("creator_id"), DataType(MySqlDbType.Int64, "11"), Column(Name:="creator_id")> Public Property creator_id As Long
    <DatabaseField("updater_id"), DataType(MySqlDbType.Int64, "11"), Column(Name:="updater_id")> Public Property updater_id As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `compounds_enzymes` (`id`, `compound_id`, `enzyme_id`, `citations`, `created_at`, `updated_at`, `creator_id`, `updater_id`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `compounds_enzymes` (`id`, `compound_id`, `enzyme_id`, `citations`, `created_at`, `updated_at`, `creator_id`, `updater_id`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `compounds_enzymes` WHERE `id` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `compounds_enzymes` SET `id`='{0}', `compound_id`='{1}', `enzyme_id`='{2}', `citations`='{3}', `created_at`='{4}', `updated_at`='{5}', `creator_id`='{6}', `updater_id`='{7}' WHERE `id` = '{8}';</SQL>
#End Region
''' <summary>
''' ```SQL
''' DELETE FROM `compounds_enzymes` WHERE `id` = '{0}';
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, id)
    End Function
''' <summary>
''' ```SQL
''' INSERT INTO `compounds_enzymes` (`id`, `compound_id`, `enzyme_id`, `citations`, `created_at`, `updated_at`, `creator_id`, `updater_id`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, id, compound_id, enzyme_id, citations, MySqlScript.ToMySqlDateTimeString(created_at), MySqlScript.ToMySqlDateTimeString(updated_at), creator_id, updater_id)
    End Function

''' <summary>
''' <see cref="GetInsertSQL"/>
''' </summary>
    Public Overrides Function GetDumpInsertValue() As String
        Return $"('{id}', '{compound_id}', '{enzyme_id}', '{citations}', '{created_at}', '{updated_at}', '{creator_id}', '{updater_id}')"
    End Function


''' <summary>
''' ```SQL
''' REPLACE INTO `compounds_enzymes` (`id`, `compound_id`, `enzyme_id`, `citations`, `created_at`, `updated_at`, `creator_id`, `updater_id`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, id, compound_id, enzyme_id, citations, MySqlScript.ToMySqlDateTimeString(created_at), MySqlScript.ToMySqlDateTimeString(updated_at), creator_id, updater_id)
    End Function
''' <summary>
''' ```SQL
''' UPDATE `compounds_enzymes` SET `id`='{0}', `compound_id`='{1}', `enzyme_id`='{2}', `citations`='{3}', `created_at`='{4}', `updated_at`='{5}', `creator_id`='{6}', `updater_id`='{7}' WHERE `id` = '{8}';
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, id, compound_id, enzyme_id, citations, MySqlScript.ToMySqlDateTimeString(created_at), MySqlScript.ToMySqlDateTimeString(updated_at), creator_id, updater_id, id)
    End Function
#End Region
Public Function Clone() As compounds_enzymes
                  Return DirectCast(MyClass.MemberwiseClone, compounds_enzymes)
              End Function
End Class


End Namespace
