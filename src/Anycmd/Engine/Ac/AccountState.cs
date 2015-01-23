﻿
namespace Anycmd.Engine.Ac
{
    using Abstractions;
    using Abstractions.Identity;
    using System;
    using Util;

    /// <summary>
    /// 表示账户业务实体类型。
    /// </summary>
    public sealed class AccountState : StateObject<AccountState>, IAccount, IAcElement
    {
        private string _name;
        private int _numberId;
        private DateTime? _createOn;
        private string _loginName;
        private string _code;
        private string _email;
        private string _mobile;
        private string _qq;
        private string _nickname;
        private string _blogUrl;

        /// <summary>
        /// 空账户
        /// </summary>
        public static readonly AccountState Empty = new AccountState(Guid.Empty)
        {
            _numberId = int.MinValue,
            _createOn = SystemTime.MinDate,
            _loginName = string.Empty,
            _code = string.Empty,
            _email = string.Empty,
            _mobile = string.Empty,
            _name = string.Empty,
            _nickname = string.Empty,
            _qq = string.Empty,
            _blogUrl = string.Empty
        };

        private AccountState(Guid id) : base(id) { }

        public static AccountState Create(AccountBase account)
        {
            if (account == null)
            {
                return Empty;
            }
            return new AccountState(account.Id)
            {
                _numberId = account.NumberId,
                _loginName = account.LoginName,
                _createOn = account.CreateOn,
                _code = account.Code,
                _email = account.Email,
                _mobile = account.Mobile,
                _name = account.Name,
                _nickname = account.Nickname,
                _qq = account.Qq,
                _blogUrl = account.BlogUrl
            };
        }

        public AcElementType AcElementType
        {
            get { return AcElementType.Account; }
        }

        public int NumberId
        {
            get { return _numberId; }
        }

        public string LoginName
        {
            get { return _loginName; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Nickname
        {
            get { return _nickname; }
        }

        public string Code
        {
            get { return _code; }
        }

        public string Email
        {
            get { return _email; }
        }

        public string Qq
        {
            get { return _qq; }
        }

        public string Mobile
        {
            get { return _mobile; }
        }

        public string BlogUrl
        {
            get { return _blogUrl; }
        }

        public DateTime? CreateOn
        {
            get { return _createOn; }
        }

        public override string ToString()
        {
            return string.Format(
@"{{
    Id:'{0}',
    NumberId:'{1}',
    LoginName:'{2}',
    Name:'{3}',
    Nickname:'{4}',
    Code:'{5}',
    Email:'{6}',
    Qq:'{7}',
    Mobile:'{8}',
    BlogUrl:'{9}',
    CreateOn:'{10}'
}}", Id, NumberId, LoginName, Name, Nickname, Code, Email, Qq, Mobile, BlogUrl, CreateOn);
        }

        protected override bool DoEquals(AccountState other)
        {
            return Id == other.Id &&
                LoginName == other.LoginName &&
                NumberId == other.NumberId &&
                Name == other.Name &&
                Nickname == other.Nickname &&
                Code == other.Code &&
                Email == other.Email &&
                Qq == other.Qq &&
                Mobile == other.Mobile &&
                BlogUrl == other.BlogUrl;
        }
    }
}
