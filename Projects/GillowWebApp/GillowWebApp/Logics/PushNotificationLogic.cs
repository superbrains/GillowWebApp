﻿using Newtonsoft.Json;
using SendingPushNotifications.Entities;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SendingPushNotifications.Logics
{
    public static class PushNotificationLogic
    {
        private static Uri FireBasePushNotificationsURL = new Uri("https://fcm.googleapis.com/fcm/send");
        private static string ServerKey = "AAAAUVh-PGM:APA91bFtbpZ39j4rDp-zVBicUn8BqEqVQMiplSRRdQA_uBKo3V0AKBnmFAcN_u65vGjybp7ZcAn2NjGqbSf5LIfuUIzPicj2MDqiiTkpFtf7-H-nGssMssQRDL0YvxiNgK4y-wPQA-bo";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public static async Task<bool> SendPushNotification(string[] deviceTokens, string title, string body, object data)
        {
            bool sent = false;

            if (deviceTokens.Count() > 0)
            {
                //Object creation

                var messageInformation = new Message()
                {
                    notification = new Notification()
                    {
                        title = title,
                        text = body
                    },
                    data = data,
                    registration_ids = deviceTokens
                };

                //Object to JSON STRUCTURE => using Newtonsoft.Json;
                string jsonMessage = JsonConvert.SerializeObject(messageInformation);

                /*
                 ------ JSON STRUCTURE ------
                 {
                    notification: {
                                    title: "",
                                    text: ""
                                    },
                    data: {
                            action: "Play",
                            playerId: 5
                            },
                    registration_ids = ["id1", "id2"]
                 }
                 ------ JSON STRUCTURE ------
                 */

                //Create request to Firebase API
                var request = new HttpRequestMessage(HttpMethod.Post, FireBasePushNotificationsURL);
                
                request.Headers.TryAddWithoutValidation("Authorization", "key=" + ServerKey);
                request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

                HttpResponseMessage result;
                using (var client = new HttpClient())
                {
                    result = await client.SendAsync(request);
                    sent = sent && result.IsSuccessStatusCode;
                }
            }

            return sent;
        }

    }
}
