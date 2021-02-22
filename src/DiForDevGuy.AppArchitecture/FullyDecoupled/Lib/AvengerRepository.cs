using Lib.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib
{
    public class AvengerRepository : IAvengerRepository, IDisposable
    {
        public AvengerRepository(IConfigurationFactory configurationFactory)
        {
            _Loggers = configurationFactory.GetLoggers();
        }

        IEnumerable<ILogger> _Loggers = null;
        
        IEnumerable<Hero> IAvengerRepository.FetchAll()
        {
            // simulate loading from datatabase
            List<Hero> heroes = new List<Hero>()
            {
                new Hero("Ironman", "Tony Stark", "Badass Geek Suit"),
                new Hero("Thor", "Thor Johnson", "Craftsman Professional Hammer"),
                new Hero("Captain America", "Steve Rogers", "Steroid Tolerance & Big Frisbee"),
                new Hero("Hulk", "Bruce Banner", "Chlorofill Induced Epidermis"),
                new Hero("Black Widow", "Natasha Romanov", "{Left blank to avoid sexism accusation}"),
                new Hero("Spiderman", "Peter Parker", "Tarzan-like Swinging Abilities")
            };

            Log("AvengerRepository.FetchAll called - Database hit.");
            
            return heroes;
        }

        Hero IAvengerRepository.Fetch(string name)
        {
            // simulate loading from datatabase
            var heroes = ((IAvengerRepository)this).FetchAll();

            Log("AvengerRepository.Fetch('{0}') called - Database hit.", name);

            return heroes.FirstOrDefault(item => item.SuperheroName.Replace(" ", "").ToLower() == name.Replace(" ", "").ToLower());
        }

        void Log(string message, params string[] args)
        {
            foreach (ILogger logger in _Loggers)
                logger.Log(message, args);
        }
        
        public void Dispose()
        {
            Log("AvengerRepository disposed.");
        }
    }
 }
