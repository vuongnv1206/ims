using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Common.Constants
{
    public static class Permissions
    {
        public static class Dashboard
        {
            [Description("Xem dashboard")]
            public const string View = "Permissions.Dashboard.View";
        }
        public static class Roles
        {
            [Description("Xem quyền")]
            public const string View = "Permissions.Roles.View";
            [Description("Tạo mới quyền")]
            public const string Create = "Permissions.Roles.Create";
            [Description("Sửa quyền")]
            public const string Edit = "Permissions.Roles.Edit";
            [Description("Xóa quyền")]
            public const string Delete = "Permissions.Roles.Delete";
        }
        public static class Users
        {
            [Description("Xem người dùng")]
            public const string View = "Permissions.Users.View";
            [Description("Tạo người dùng")]
            public const string Create = "Permissions.Users.Create";
            [Description("Sửa người dùng")]
            public const string Edit = "Permissions.Users.Edit";
            [Description("Xóa người dùng")]
            public const string Delete = "Permissions.Users.Delete";
        }

        public static class Project
        {
            [Description("Xem dự án")]
            public const string View = "Permissions.Project.View";
            [Description("Tạo mới dự án")]
            public const string Create = "Permissions.Project.Create";
            [Description("Sửa dự án")]
            public const string Edit = "Permissions.Project.Edit";
            [Description("Xóa dự án")]
            public const string Delete = "Permissions.Project.Delete";
        }

        public static class Class
        {
            [Description("Xem lớp học")]
            public const string View = "Permissions.Class.View";
            [Description("Tạo mới lớp học")]
            public const string Create = "Permissions.Class.Create";
            [Description("Sửa lớp học")]
            public const string Edit = "Permissions.Class.Edit";
            [Description("Xóa lớp học")]
            public const string Delete = "Permissions.Class.Delete";
        }

        public static class Assignment
        {
            [Description("Xem bài tập")]
            public const string View = "Permissions.Assignment.View";
            [Description("Tạo mới bài tập")]
            public const string Create = "Permissions.Assignment.Create";
            [Description("Sửa bài tập")]
            public const string Edit = "Permissions.Assignment.Edit";
            [Description("Xóa bài tập")]
            public const string Delete = "Permissions.Assignment.Delete";
        }

        public static class Subject
        {
            [Description("Xem môn học")]
            public const string View = "Permissions.Subject.View";
            [Description("Tạo mới môn học")]
            public const string Create = "Permissions.Subject.Create";
            [Description("Sửa môn học")]
            public const string Edit = "Permissions.Subject.Edit";
            [Description("Xóa môn học")]
            public const string Delete = "Permissions.Subject.Delete";
        }

        public static class Issue
        {
            [Description("Xem issue")]
            public const string View = "Permissions.Issue.View";
            [Description("Tạo mới issue")]
            public const string Create = "Permissions.Issue.Create";
            [Description("Sửa issue")]
            public const string Edit = "Permissions.Issue.Edit";
            [Description("Xóa issue")]
            public const string Delete = "Permissions.Issue.Delete";
        }

        public static class Milestone
        {
            [Description("Xem Milestone")]
            public const string View = "Permissions.Milestone.View";
            [Description("Tạo mới Milestone")]
            public const string Create = "Permissions.Milestone.Create";
            [Description("Sửa Milestone")]
            public const string Edit = "Permissions.Milestone.Edit";
            [Description("Xóa Milestone")]
            public const string Delete = "Permissions.Milestone.Delete";
        }
    }
}