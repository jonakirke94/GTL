using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using GTL.Application.Helper;
using Xunit;

namespace Application.Tests
{
    public class HashingTests
    {
        [Fact]
        public void Untampered_hash_matches_the_text()
        {
            // Arrange  
            const string message = "passw0rd";
            var salt = Hasher.CreateSalt();
            var hash = Hasher.Hash(message, salt);

            // Act  
            var match = Hasher.Validate(message, salt, hash);

            // Assert  
            Assert.True(match);
        }

        [Fact]
        public void Tampered_hash_does_not_matche_the_text()
        {
            // Arrange  
            const string message = "passw0rd";
            var salt = Hasher.CreateSalt();
            const string hash = "blahblahblah";

            // Act  
            var match = Hasher.Validate(message, salt, hash);

            // Assert  
            Assert.False(match);
        }

        [Fact]
        public void Hash_of_two_different_messages_dont_match()
        {
            // Arrange  
            const string message1 = "passw0rd";
            const string message2 = "password";
            var salt = Hasher.CreateSalt();

            // Act  
            var hash1 = Hasher.Hash(message1, salt);
            var hash2 = Hasher.Hash(message2, salt);

            // Assert  
            Assert.NotEqual(hash1, hash2);
        }
    }
}
