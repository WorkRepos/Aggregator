# Aggregator

Адреса Rss:
https://www.belta.by/rss
https://synth.market/rss-2

Контроллеры:
api/NewsAggregator/{skip}/{take}                    //GET
                                                    //для вывода списка всех новостей
                                                    
api/NewsAggregator/{searchText}/{skip}/{take}       //POST
                                                    //для поиска с выводом всех новостей
                                                    
api/RssUrlAggregator                                //POST
                                                    //для добавления rss url
                                                    
api/RssUrlAggregator/{id}                           //DELETE
                                                    //для удаления rss url
